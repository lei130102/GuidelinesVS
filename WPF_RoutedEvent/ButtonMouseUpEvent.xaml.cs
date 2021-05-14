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

namespace WPF_RoutedEvent
{
    /// <summary>
    /// ButtonMouseUpEvent.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonMouseUpEvent : Window
    {
        public ButtonMouseUpEvent()
        {
            InitializeComponent();

            //通过下面的代码(注意，没有对应的XAML的解决方法)，仍可以处理已挂起的事件：
            cmd.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(Backdoor), true);////AddHandler()方法提供了一个重载版本，该版本可以接收一个Boolean值作为他的第三个参数。如果将该参数设置为true，那么即使设置了Handled标志，也将接收到事件
        }

        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button.Click事件处理。Button.Click事件可以通过鼠标单击触发，也可以通过键盘触发");
        }

        private void cmd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("该事件处理永远不会被调用");
        }

        private void Backdoor(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button.MouseUp事件处理");
        }
    }
}
