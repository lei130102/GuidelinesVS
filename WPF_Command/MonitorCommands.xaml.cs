using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


//比价困难的是将命令历史集成进WPF命令模型中，理想的解决方案是使用能跟踪任意命令的方式完成该任务，而不管命令是被如何触发和绑定的。相对不理想的解决方案是，
//强制依赖于一整套全新的自定义命令对象(这一逻辑功能内置到这些自定义命令对象中)，或手动处理每个命令的Executed事件

//响应特定的命令是非常简单的，但当执行任何命令时如何进行响应呢？
//技巧是使用CommandManager类，该类提供了几个静态事件。这些事件包括CanExecute、PreviewCanExecute、Executed以及PreviewExecuted。在本例中，Executed
//和PreviewExecuted事件最有趣，因为每当执行任何一个命令时都会引发他们

//尽管CommandManager类挂起了Executed事件，但仍可使用UIElement.AddHandler()方法关联事件处理程序，并为可选的第三个参数传递true值。这样将允许接收事件，
//即使事件已经被处理过也同样如此。然而Executed事件是在命令执行完之后触发的，这时已经来不及在命令历史中保存被影响的控件的状态了。相反，需要相响应PreviewExecuted
//事件，该事件在命令执行前一个被触发

namespace WPF_Command
{
    /// <summary>
    /// 假设提供EditingCommands.Backspace命令，而且用户在一行中回退了几个空格。可通过向最近命令堆栈中添加Backspace命令来记录这一操作，但实际上每次添加的是
    /// 相同的命令对象。因此，没有简单的方法用于存储命令的其他信息，例如刚刚删除的字符。如果希望存储该状态，需要构建自己的数据结构。本例使用名为CommandHistoryItem的类
    ///
    /// 每个对象跟踪以下几部分信息：
    /// 1.命令名称
    /// 2.执行命令的元素(在本例中，有两个文本框，所以可以是其中的任意一个)
    /// 3.在目标元素中被改变了的属性。在本例中是TextBox类的Text属性
    /// 4.可用于保存受影响元素以前状态的对象(例如，执行命令之前文本框中的文本)
    /// (这一设计非常巧妙，可以为元素存储状态。如果存储整个窗口状态的快照，那么会显著增加内存的使用量。然而，如果具有大量数据（比如文本框有几十行文本），Undo
    /// 操作的负担就很大了。解决方法是限制在历史中存储的项的数量，或使用更加智能(也更复杂)的方法只存储被改变的数据的信息，而不是存储所有数据)
    /// </summary>
    public class CommandHistoryItem
    {
        public string CommandName
        {
            get;
            set;
        }

        public UIElement ElementActedOn
        {
            get;
            set;
        }

        public string PropertyActedOn
        {
            get;
            set;
        }

        public object PreviousState
        {
            get;
            set;
        }

        public CommandHistoryItem(string commandName)
            : this(commandName, null, "", null)
        { }

        public CommandHistoryItem(string commandName, UIElement elementActedOn, string propertyActedOn, object previousState)
        {
            CommandName = commandName;
            ElementActedOn = elementActedOn;
            PropertyActedOn = propertyActedOn;
            PreviousState = previousState;
        }

        public bool CanUndo
        {
            get
            {
                return (ElementActedOn != null && PropertyActedOn != "");
            }
        }

        /// <summary>
        /// 使用反射为修改过的属性应用以前的值，用于恢复TextBox控件中的文本
        /// 但对于更复杂的应用程序，需要使用CommandHistoryItem类的层次结构，每个类都可以使用不同方式翻转不同类型的操作
        /// </summary>
        public void Undo()
        {
            Type elementType = ElementActedOn.GetType();
            PropertyInfo property = elementType.GetProperty(PropertyActedOn);
            property.SetValue(ElementActedOn, PreviousState, null);
        }
    }
        

    /// <summary>
    /// MonitorCommands.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorCommands : Window
    {
        static MonitorCommands()
        {
            applicationUndo = new RoutedUICommand("ApplicationUndo", "Application Undo", typeof(MonitorCommands));
        }

        /// <summary>
        /// 执行应用程序范围内Undo操作的命令
        /// 
        /// 使用ApplicationCommands.Undo命令是不合适的，原因是为了达到不同的目的，他已经被用于单独的文本框控件(翻转最后的编辑变化)。相反，需要创建一个新命令
        /// </summary>
        private static RoutedUICommand applicationUndo;
        public static RoutedUICommand ApplicationUndo
        {
            get
            {
                return MonitorCommands.applicationUndo;
            }
        }

        public MonitorCommands()
        {
            InitializeComponent();

            //在窗口的构造函数中关联CommandManager.PreviewExecuted事件处理程序
            this.AddHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(CommandExecuted));
        }

        private void window_Unloaded(object sender, RoutedEventArgs e)
        {
            //当关闭窗口时解除关联CommandManager.PreviewExecuted事件处理程序
            this.RemoveHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(CommandExecuted));
        }

        private void CommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //当触发CommandManager.PreviewExecuted事件时，需要确定准备执行的命令是否是我们所关心的，如果是，可创建CommandHistoryItem对象，并将
            //其添加到Undo堆栈中

            //注意两个潜在问题：
            //1.当单击工具栏按钮以在文本框上执行命令时，CommandManager.PreviewExecuted事件被引发了两次——一次是针对工具栏按钮，另一次是针对文本框
            //下面的代码通过忽略发送者是ICommandSource的命令，避免在Undo历史中重复条目
            //2.需要明确忽略不希望添加到Undo历史中的命令
            //例如ApplicationUndo命令，通过该命令可翻转上一步操作


            // Ignore menu button source.
            if (e.Source is ICommandSource)
            {
                return;
            }

            // Ignore the ApplicationUndo command.
            if(e.Command == MonitorCommands.ApplicationUndo)
            {
                return;
            }

            // Could filter for commands you want to add to the stack (for example, not selection events)

            TextBox txt = e.Source as TextBox;
            if(txt != null)
            {
                RoutedCommand cmd = (RoutedCommand)e.Command;

                CommandHistoryItem historyItem = new CommandHistoryItem(cmd.Name, txt, "Text", txt.Text);

                ListBoxItem item = new ListBoxItem();
                item.Content = historyItem;
                lstHistory.Items.Add(historyItem);
                //本例在ListBox控件中存储所有CommandHistoryItem对象。ListBox控件的DisplayMember属性被设置为Name，因而会显示每个条目的CommandHistoryItem.Name属性

                //CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// 为恢复最近的修改，只需要调用CommandHistoryItem对象的Undo()方法，然后从列表中删除该项即可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationUndoCommand_Executed(object sender, RoutedEventArgs e)
        {
            CommandHistoryItem historyItem = (CommandHistoryItem)lstHistory.Items[lstHistory.Items.Count - 1];
            if(historyItem.CanUndo)
            {
                historyItem.Undo();
            }
            lstHistory.Items.Remove(historyItem);
        }

        /// <summary>
        /// 确保只有当在Undo历史中至少有一项时，才能执行此代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationUndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(lstHistory == null || lstHistory.Items.Count == 0)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

    }
}

//在实际应用程序中使用本例提供的方法还需要进行许多改进。例如，需要耗费大量时间改进CommandManager.PreviewExecuted事件的处理程序，以忽略那些明显不需要跟踪的命令
//(当前，诸如使用键盘选择文本的事件以及单击空格键引发的命令等)。类似地，可能希望为那些不是由命令表示的但应当被翻转的操作添加CommandHistoryItem对象。例如，输入一些
//文本，然后导航到其他控件。最后，可能希望将Undo历史限制在最近执行的命令范围之内
