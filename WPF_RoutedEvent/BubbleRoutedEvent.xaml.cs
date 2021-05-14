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
    /// BubbleRoutedEvent.xaml 的交互逻辑
    /// </summary>
    public partial class BubbleRoutedEvent : Window
    {
        public BubbleRoutedEvent()
        {
            InitializeComponent();
        }

        protected int eventCounter = 0;

        private void Someone_MouseUp(object sender, MouseButtonEventArgs e)//因为这里不使用MouseButtonEventArgs中与鼠标相关的附加信息，所以这里也可以使用RoutedEventArgs
        {
            eventCounter++;

            string message = "#" + eventCounter.ToString() + ":\r\n" +
                " Sender:" + sender.ToString() + "\r\n" +
                " Source:" + e.Source + "\r\n" +                                                         //        RoutedEventArgs.Source:指示引发了事件的对象
                " Original Source:" + e.OriginalSource;                                                  //RoutedEventArgs.OriginalSource:指示最初是什么对象引发了事件(例如，如果单击窗口边框上的关闭按钮，事件源为Window对象，但事件最原始的源是Border对象。这是因为Window对象是由多个单独的、更小的部分构成的)

            lstMessages.Items.Add(message);
            e.Handled = (bool)chkHandle.IsChecked;//IsChecked的类型是bool?   这里使用显示类型转换为bool     //      RoutedEventArgs.Handled:该属性允许终止事件的冒泡或隧道过程。如果控件将Handled属性设为true，那么事件就不会继续传递
                                                                                                         //  RoutedEventArgs.RoutedEvent:通过事件处理程序为触发的事件提供RoutedEvent对象(如静态的UIElement.MouseUpEvent对象)。如果用同一个事件处理程序处理不同的事件，这一信息是非常有用的
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            eventCounter = 0;
            lstMessages.Items.Clear();
        }
    }
}
