using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


//添加初始界面的方法：
//1.为项目添加图像文件(通常是.bmp、.png或者.jpg文件)
//2.在Solution Explorer中选择图像文件
//3.将Build Action修改为SplashScreen

//在obj/Debug目录下的App.g.cs文件中自动生成如下代码：

///// <summary>
///// Application Entry Point.
///// </summary>
//[System.STAThreadAttribute()]
//[System.Diagnostics.DebuggerNonUserCodeAttribute()]
//[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
//public static void Main()
//{
//    SplashScreen splashScreen = new SplashScreen("%e6%b5%8b%e8%af%95%e5%9b%be%e7%89%87.jfif");
//    splashScreen.Show(true);
//    WPF_SplashScreen.App app = new WPF_SplashScreen.App();
//    app.InitializeComponent();
//    app.Run();
//}

//通过上面也可以看出，可自行编写这一简短逻辑，而不是使用SplashScreen生成操作。但有一点需要指出，可以改变的唯一细节是初始界面褪去的速度。
//为此，需要向SplashScreen.Show()方法传递false(从而使WPF不会自动淡入初始界面)。然后由您负责通过调用SplashScreen.Close()方法在恰当的
//时机隐藏初始界面，并提供TimeSpan值来指示经过多长时间淡出初始界面

namespace WPF_SplashScreen
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
