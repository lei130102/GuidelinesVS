using System;
using System.Collections.Generic;
using System.Linq;
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

//命令

//路由事件是非常低级的元素。在实际应用程序中，功能被划分成一些高级的任务。这些任务可通过各种不同的动作和用户界面元素触发，包括主菜单、上下文菜单、
//键盘快捷键以及工具栏。可在WPF中定义这些任务——所谓的命令——并将控件连接到命令，从而不需要重复编写事件处理代码。更重要的是，当连接的命令不可用时，
//命令特性通过自动禁用控件来管理用户界面的状态。命令模型还为存储和本地化命令的文本标题提供了一个中心场所

//许多应用程序任务可通过各种不同的路由触发，所以经常需要编写多个事件处理程序来调用相同的应用程序方法，没有减少任何工作，当需要处理用户界面的状态时，
//问题就更复杂了

//(在设计良好的Windows应用程序中，应用程序逻辑不应位于事件处理程序中，而应在更高层的方法中编写代码)

//WPF命令没有解决的问题：
//1.命令跟踪(例如，保留最近命令的历史)
//2.“可撤销的”命令
//3.具有状态并可处于不同“模式”的命令;例如，可被打开或关闭的命令

//WPF命令模型由四部分组成
//1.命令
//命令表示应用程序命令，并且跟踪任务是否能够被执行。然而，命令实际上不包含执行应用程序任务的代码
//2.命令绑定
//每个命令绑定针对用户界面的具体区域，将命令连接到相关的应用程序逻辑。这种分解的设计师非常重要的，因为单个命令可用于应用程序中的多个地方，并且在每个地方
//具有不同的意义。为处理这一问题，需要将同一命令与不同的命令绑定
//3.命令源
//命令源触发命令。例如MenuItem和Button都是命令源，单击他们都会执行绑定命令
//3.命令目标
//命令目标是在其中执行命令的元素。例如，Paste命令可在TextBox控件中插入文本，而OpenFile命令可在DocumentViewer中打开文档。根据命名的本质，目标可能很重要，
//也可能不重要

//命令(ICommand)实际上不包含执行应用程序任务的代码，为触发命令，需要有命令源(IComandSource)(也可使用代码)，为响应命名，需要有命令绑定(CommandBinding)，命令绑定将执行转发给普通的事件处理程序






//ICommand接口
//WPF命令模型的核心是System.Windows.Input.ICommand接口，该接口定义了命令的工作原理。该接口包含两个方法和一个事件：
//public interface ICommand
//{
//    void Execute(object parameter);
//    bool CanExecute(object parameter);
//  
//    //当命令状态改变时引发CanExecuteChanged事件。对于使用命令的任何控件，这是指示信号，表示他们应当调用CanExecute()方法检查命令的状态。
//    //通过使用该事件，当命令可用时，命令源(如Button或者MenuItem)可自动启用自身；当命令不可用时，禁用自身
//    event EventHandler CanExecuteChanged;
//}









//RoutedCommand类

//当创建自己的命令时，不会直接实现ICommand接口；而是使用System.Windows.Input.RoutedCommand类，该类自动实现了ICommand接口。
//RoutedCommand类是WPF中唯一实现了ICommand接口的类。换句话说，所有WPF命令都是RoutedCommand类及其派生类的实例

//RoutedCommand类不包含任何应用程序逻辑，而只代表命令，这意味着各个RoutedCommand对象具有相同的功能

//RoutedCommand类为事件冒泡和隧道添加了一些额外的基础结构。鉴于ICommand接口封装了命令的思想——可被触发的动作并可被启用或禁用——RoutedCommand类
//对命令进行了修改，使命令可在WPF元素层次结构中冒泡，以便获得正确的事件处理程序
//(
//WPF为什么需要事件冒泡？
//当第一次查看WPF命令模型时，很难确切地理解WPF命令为什么需要路由事件。毕竟，不管命令是如何被调用的，不应由命令对象负责执行命令吗？
//如果直接使用ICommand接口创建自己的命令类，确实如此，代码应当被硬链接到命令，从而可以通过相同的方式工作，而不管是什么操作触发了命令，这时不需要事件冒泡。比如第三方库提供的RelayCommand类
//然而，WPF使用了大量预先构建的命令。这些命令类没有包含任何实际代码，他们只是为了方便地定义代表常见应用程序任务(如打印文档)的对象。为使用这些命令，需要使用命令绑定，为代码引发事件。为确保
//可在某个位置处理事件，甚至事件是由同一窗口中的不同命令源引发的，需要使用事件冒泡的功能

//这又导致另一个有趣的问题：为什么非要使用预先构建的命令呢？使用自定义的命令类处理所有工作不是更清晰吗？为什么反而要依赖于事件处理程序呢？
//对于大多数情况，这种设计可能更简单。然而，使用预先构建命令的优点是，他们为集成提供了更好的可能。例如，第三方开发人员可创建使用预先构建的Print命令的文档查看控件。
//只要应用程序使用相同的预先构建的命令，在应用程序中关联打印时就不需要做任何额外工作了。如此看来，命令是WPF可插入体系架构的主要组成部分之一
//)
//为支持路由事件，RoutedCommand类私有地实现了ICommand接口，并添加了ICommand接口方法的一些不同版本
//public void Execute(object parameter, IIputElement target)
//{...}
//public bool CanExecute(object parameter, IIputElement target)
//{...}
//参数target是开始处理事件的元素。事件从target元素开始，然后冒泡至高层的容器，直到应用程序为了执行合适的任务而处理了事件(为了处理Executed事件，元素还需要借助于另一个类——CommandBinding类的帮助)

//RoutedCommand类还引入了三个属性：
//1.命令名称(Name属性)
//2.包含命令的类(OwnerType)
//3.以及任何可用于触发命令的按键或鼠标操作(位于InputGestures集合中)








//RoutedUICommand类

//RoutedUICommand类继承自RoutedCommand类(实际上，WPF提供的所有预先构建好的命令都是RoutedUICommand对象)

//RoutedUICommand类用于具有文本的命令，这些文本显示在用户界面中的某些地方(例如菜单项文本、工具栏按钮的工具提示)。
//RoutedUICommand类只增加了Text属性，该属性是为命令显示的文本
//为命令定义命令文本(而不是直接在控件上定义文本)的优点是可在某个位置执行本地化。但如果命令文本永远不会再用户界面的任何地方显示，那么RoutedUICommand类
//和RoutedCommand类是等效的
//(注意，不见得要在用户界面中使用RoutedUICommand文本。实际上，可能有更好的原因使用其他内容。例如，可能更愿意使用PrintDocument而不只是Print，而且在某些
//情况下完全可以用小图形代替文本)






//命令库

//WPF提供了基本命令库，这些命令通过以下5个专门的静态类的静态属性提供：
//1.ApplicationCommands
//2.NavigationCommands
//3.EditingCommands
//4.ComponentCommands
//5.MediaCommands

//例如，ApplicationCommands.Open是提供RoutedUICommand对象的静态属性，该对象表示应用程序中的Open命令。因为ApplicationCommands.Open是静态属性，所以在整个应用程序中只有一个
//Open命令实例。然而，根据命令源的不同(换句话说，是在用户界面的什么地方触发的该命令)，可采用不同的处理方式

//例如，ApplicationCommands.SelecteAll命令的文本是Select All(Name属性使用相同的没有空格的文本)

//例如，ApplicationCommands.Open是ApplicationCommands类的静态属性，所以RoutedUICommand.OwnerType属性返回ApplicationCommands类的类型对象
//(在将命令绑定到窗口前，可修改命令的Text属性(例如，在窗口或应用程序类的构造函数中使用代码)。因为命令是在整个应用程序范围内全局使用的静态对象，所以改变命令
//文本会影响在用户界面中所有位置显示的命令。与Text属性不同，不能修改Name属性)

//例如，ApplicationCommands.Open命令被映射到Ctrl+O快捷键。只要将命令绑定到命令源，并为窗口添加命令源，这个快捷键就会被激活，即使没有在用户界面的任何地方显示该命令也同样如此
//这些单独的命令对象仅是一些标志器，不具有实际功能。然而，许多命令对象都有一个额外的特征：默认输入绑定
namespace WPF_Command
{
    /// <summary>
    /// TestNewCommand.xaml 的交互逻辑
    /// </summary>
    public partial class TestNewCommand : Window
    {
        public TestNewCommand()
        {
            //ApplicationCommands.New.Text = "Completely New";

            InitializeComponent();

            //CommandBinding bindingNew = new CommandBinding(ApplicationCommands.New);
            //bindingNew.Executed += NewCommand;

            //this.CommandBindings.Add(bindingNew);
        }

        private void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            //ExecutedRoutedEventArgs.Command           获得被调用的命令的引用
            //ExecutedRoutedEventArgs.Parameter         同时传递的额外数据
            //                                          本例中，因为没有传递任何额外的数据，所以参数为null(如果希望传递附加数据，应设置命令源的CommandParameter属性；并且
            //                                          如果希望传递一些来自另一个控件的信息，还需要使用数据绑定表达式设置CommandParameter属性)

            MessageBox.Show("New command triggered by " + e.Source.ToString());
        }

        private void DoCommand_Click(object sender, RoutedEventArgs e)
        {
            this.CommandBindings[0].Command.Execute(null);
            //ApplicationCommands.New.Execute(null, (Button)sender);
        }
    }
}
