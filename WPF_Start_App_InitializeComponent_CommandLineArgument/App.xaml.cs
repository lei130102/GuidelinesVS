using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

        private static void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            if(e.Args.Length > 0)
            {
                string file = e.Args[0];
                if(System.IO.File.Exists(file))
                {
                    //
                }
            }
            else
            {
                // (Perform alternate initialization here when
                // no command-line arguments are supplied.)
            }
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
