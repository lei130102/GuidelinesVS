using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

/*

应用程序事件

处理事件时有两种选择：
1.关联事件处理程序
2.重写相应的受保护方法(注意对于DispatcherExceptionUnhandled事件，该事件没有相应的OnDispatcherExceptionUnhandled()方法，所以始终需要使用事件处理程序)

当重写应用程序方法时，最好首先调用基类的实现，通常，基类的实现只是引发相应的应用程序事件









自己总结顺序：

调用Application.Run() -> 触发Startup事件(1.检查命名行参数 2.创建和显示主窗口) -> 主窗口显示

导致应用程序关闭的操作(不管因为什么原因) -> 触发Exit事件(1.设置从Run()方法返回的整数类型的退出码) -> Application.Run()函数返回 -> 应用程序关闭

用户注销或关闭计算机 -> 触发SessionEnding事件(1.取消关闭应用程序) -> WPF将自动调用Application.Shutdown()方法(导致应用程序关闭的操作)

激活应用程序中的窗口(从另一个Windows程序切换到该应用程序 或 第一次显示窗口) -> 触发Activated事件

取消激活应用程序中的窗口(切换到另一个Windows程序时) -> 触发 Deactivated事件
*/
namespace WPF_ApplicationEvent
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Startup事件
        ///
        /// 该事件在调用Application.Run()方法之后，并且在主窗口显示之前(如果把主窗口传递给Run()方法)发生。可使用该事件检查所有命令行参数，命令行参数是通过StartupEventArg.Args
        /// 属性作为数组提供的。还可使用该事件创建和显示主窗口(而不是使用App.xaml文件中的StartUri属性)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        /// <summary>
        /// Exit事件
        /// 
        /// 该事件在应用程序关闭时(不管是因为什么原因)，并在Run()方法即将返回之前发生。此时不能取消关闭，但可以通过代码在Main()方法中
        /// 重新启动应用程序。可使用Exit事件设置从Run()方法返回的整数类型的退出代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }

        /// <summary>
        /// SessionEnding事件
        /// 
        /// 该事件在Windows对话结束时发生——例如，当用户注销或关闭计算机时（通过检查SessionEndingCancelEventArgs.ReasonSessionEnding属性可以确定原因
        /// 也可通过将SessionEndingEventArgs.Cancel属性设置为true来取消关闭应用程序。否则，当事件处理程序结束时，WPF将调用Application.Shutdown()方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {

        }

        /// <summary>
        /// Activated事件
        ///
        /// 当激活应用程序中的窗口时发生该事件。当从另一个Windows程序切换到该应用程序时会发生该事件。当第一次显示窗口时也会发生该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Activated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Deactivated事件
        ///
        /// 当取消激活应用程序中的窗口时发生该事件。当切换到另一个Windows程序时也会发生该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Deactivated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// DispatcherUnhandledException事件
        /// 
        /// 在应用程序(主应用程序线程)中的任何位置，只要发生未处理的异常，就会发生该事件(应用程序调用程序会捕获这些异常)。通过响应该事件，
        /// 可记录重要错误，甚至可选择不处理这些异常，并通过将DispatcherUnhandledExceptionEventArgs.Handled属性设置为true继续运行应用程序。
        /// 只有当可以确保应用程序仍然处于合法状态并且可以继续运行时，才可以这样处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }
    }
}
