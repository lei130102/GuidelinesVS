using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//跟WPF_Start_App_InitializeComponent项目类似，去掉了InitializeComponent函数，这里只是补充了命令行参数获取

namespace WPF_Start_App_InitializeComponent_CommandLineArgument
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }

        //为处理命名行参数，需要响应Application.Startup事件。命令行参数是通过StartupEventArgs.Args属性作为字符串数组提供的
        private static void App_Startup(object sender, StartupEventArgs e)
        {
            //在本例中，没有在任何地方设置Application.StartupUri属性——而是使用代码实例化主窗口

            // Create, but don't show the main window.
            MainWindow window = new MainWindow();
            if (e.Args.Length > 0)
            {
                string file = e.Args[0];
                if (System.IO.File.Exists(file))
                {
                    // Configure the main window.
                }
            }
            else
            {
                // (Perform alternate initialization here when
                // no command-line arguments are supplied.)
            }
            // This window will automatically be set as the Application.MainWindow.
            window.Show();
        }

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            App app = new App();
            app.Run();
        }
    }
}
