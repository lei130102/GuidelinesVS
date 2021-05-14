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

//窗口(Window)除了有Initialized、Loaded和Unloaded事件外，还有自己更特殊的生命周期事件

namespace WPF_RoutedEvent.WPF_Event
{
    /// <summary>
    /// WindowLifeCycleEvent.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLifeCycleEvent : Window
    {
        public WindowLifeCycleEvent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// SourceInitialized 事件
        /// 当取得窗口的HwndSource属性时(但在窗口可见之前)发生。HwndSource是窗口句柄，如果调用Win32 API中的遗留函数，就可能需要使用该句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SourceInitialized(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ContentRendered 事件
        /// 在窗口首次呈现后立即发生。对于执行任何可能会影响窗口可视外观的更改操作，这不是一个好位置，否则将会强制进行第二次呈现（改用Loaded事件）。然而，ContentRendered事件表明窗口已经完全可见，并且已经准备好接收输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Activated 事件
        /// 当用户切换到该窗口时发生（例如，从应用程序的其他窗口或从其他应用程序切换到该窗口）。当窗口第一次加载时也会引发Activated事件。从概念上讲，窗口的Activated事件相当于控件的GotFocus事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Deactivated 事件
        /// 当用户从该窗口切换到其他窗口时发生（例如，切换到应用程序的其他窗口或切换到其他应用程序）。当用户关闭窗口时也会发生该事件，该事件在Closing事件之后，但在Closed事件之前发生。从概念上讲，窗口的Deactivated事件相当于控件的LostFocus事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Deactivated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Closing 事件
        /// 当关闭窗口是发生，不管是用户关闭窗口还是通过代码调用Window.Close()或Application.Shutdown()方法关闭窗口。Closing事件提供了取消操作并保持打开状态的机会，具体通过将
        /// CancelEventArgs.Cancel属性设置为true实现该目标。但是，如果是因为用户关闭或注销计算机而导致应用程序被关闭，就不能接收到Closing事件。为应对这种情况，需要处理将在Application.SessionEnding事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        /// <summary>
        /// Closed 事件
        /// 当窗口已经关闭后发生。但是，此时仍可以访问元素对象，当然是在Unloaded事件尚未发生之前。在此，可以执行一些清理工作，向永久存储位置（如配置文件或Windows注册表）写入设置信息等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
