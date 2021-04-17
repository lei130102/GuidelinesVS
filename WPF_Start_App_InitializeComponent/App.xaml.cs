using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//原理：

//删除obj\Debug目录下的App.g.cs中的两个函数：

///// <summary>
///// InitializeComponent
///// </summary>
//[System.Diagnostics.DebuggerNonUserCodeAttribute()]
//[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
//public void InitializeComponent()
//{

//#line 5 "..\..\App.xaml"
//    this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);

//#line default
//#line hidden
//}

///// <summary>
///// Application Entry Point.
///// </summary>
//[System.STAThreadAttribute()]
//[System.Diagnostics.DebuggerNonUserCodeAttribute()]
//[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
//public static void Main()
//{
//    WPF_Start_App_InitializeComponent.App app = new WPF_Start_App_InitializeComponent.App();
//    app.InitializeComponent();
//    app.Run();
//}

//然后在这里补充

namespace WPF_Start_App_InitializeComponent
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
        }
    }
}
