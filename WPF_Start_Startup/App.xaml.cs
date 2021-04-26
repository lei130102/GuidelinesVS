using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//修改App.xaml中为 Startup="Application_Startup" 

//自动生成的obj/debug中的App.g.cs中：
///// <summary>
///// InitializeComponent
///// </summary>
//[System.Diagnostics.DebuggerNonUserCodeAttribute()]
//[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
//public void InitializeComponent()
//{

//#line 5 "..\..\App.xaml"
//    this.Startup += new System.Windows.StartupEventHandler(this.Application_Startup);

//#line default
//#line hidden
//}

namespace WPF_Start_Startup
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //MainWindow window = new MainWindow();
            ////WPF 中的命令行参数
            //if(e.Args.Length == 1)
            //{
            //    MessageBox.Show("Now opening file: \n\n" + e.Args[0]);
            //}
            //window.Show();

            //或者

            this.StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute);
            //如果Application_Startup是静态函数，那么 Application.Current.StartupUri = ...
        }
    }
}
