using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CodeMethodCodeAndXAML2BAML
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}


//当编译WPF应用程序时，Visual Studio使用分为两个阶段的编译处理过程。

//第一阶段将XAML文件编译为BAML。例如，如果项目中包含名为Window1.xaml的文件，编译器将创建名为Window1.baml的
//临时文件，并将该文件放在项目文件夹的obj/Debug子文件夹中。同时，使用选择的语言为窗口创建部分类。例如，如果使用
//C#语言，编译器将在obj\Debug文件夹中创建名为Window1.g.cs的文件。g代表生成的(generated)
//部分类包括如下三部分内容：
//1.窗口中所有控件的字段
//2.从程序集中加载BAML的代码，由此创建对象树。当构造函数调用InitializeComponent()方法时将发生这种情况
//3.将恰当的控件对象指定给各个字段以及连接所有事件处理程序的代码。该过程是在名为Content()的方法中完成的，
//BAML解析器在每次发现一个已经命名的对象时调用该方法一次
//(部分类不包含实例化和初始化控件的代码，因为这项任务由WPF引擎在使用Application.LoadComponent()方法处理BAML时执行)

//第二阶段，当“从XAML到BAML”的编译阶段结束后，Visual Studio使用合适的语言编译器来编译代码和生成的部分类文件。对于
//C#应用程序而言，使用csc.exe编译器处理这一任务。编译过的代码会变成单个程序集(对于Eight Ball Answer示例，是EightBall.exe程序集)
//而且每个窗口的BAML都作为独立资源被嵌入到程序集中
