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

//自定义命令
//本例定义了自定义命令requery，类型为RoutedUICommand，注意是静态字段，XAML中使用的是对应的静态属性

//注意，当使用自定义命令时，可能需要调用静态的CommandManager.InvalidateRequerySuggested()方法，通知WPF重新评估命令的状态，
//然后WPF会触发CanExecute事件，并更新使用该命令的任意命令源

namespace WPF_Command
{
    public class DataCommands
    {
        private static RoutedUICommand requery;

        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));

            requery = new RoutedUICommand("Requery", "Requery", typeof(DataCommands), inputs);

            //也可修改已有命令的RoutedCommand.InputGestures集合——例如，删除已有的键绑定或添加新的键绑定。甚至可添加鼠标绑定，
            //从而当同时按下鼠标键和键盘上的修饰键时触发命令(尽管对于这种情况，可能希望只将命令绑定放置到鼠标处理起作用的元素上)
        }

        public static RoutedUICommand Requery
        {
            get
            {
                return requery;
            }
        }
    }

    /// <summary>
    /// CustomCommand.xaml 的交互逻辑
    /// </summary>
    public partial class CustomCommand : Window
    {
        public CustomCommand()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Requery");
        }
    }
}
