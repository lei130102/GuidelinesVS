using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//程序集资源(又称为二进制资源，因为他们作为不透明的二进制数据被嵌入到已编译的程序集中)

//WPF应用程序中的程序集资源与其他.NET应用程序中的程序集资源在本质上是相同的。基本概念是为项目添加文件，从而Visual Studio可将其嵌入到
//编译过的应用程序的EXE或者DLL文件中。WPF程序集资源与其他引用程序中的程序集资源之间的重要区别是引用他们的寻址系统不同















//添加资源
//在项目中创建子文件夹，组织不同类型的资源，将子文件夹中添加文件，并在Properties窗口中将其Build Action属性设置为Resource来添加自己的资源
//(以这种方式添加的资源易于更新。只需要替换文件并重新编译应用程序即可)

//为成功地使用程序集资源，务必注意以下两点：
//a.不能将Build Action属性错误地设置为Embedded Resource。尽管所有程序集资源都被定义为嵌入的资源，但Embedded Resource生成操作会在另一个更难
//访问的位置放置二进制数据。在WPF应用程序中，假定总是使用Resource生成类型
//b.不要在Project Properties窗口中使用Resource选项卡。WPF不支持这种类型的资源URI

//WPF将用户自定义资源和其他BAML资源合并到单独的流中。单独的资源流使用以下格式命名：AssemblyName.g.resources
//(比如本例就是WPF_AssemblyResources.g.resources)

//如果想要实际查看在编译过的程序集中嵌入的资源，可使用反编译工具。但主要的.NET工具——ildasm——不支持此功能。然而，可使用诸如Reflector的更出色
//工具来深入挖掘资源
//除所有图像和音频文件外，还可以看到用于应用程序中窗口的BAML资源。在WPF中，文件名中的空格不会引起问题，因为Visual Studio足够智能，他能够正确地
//忽略他们。当应用程序被编译过之后，您可能还会注意到文件名变成了小写形式
















//检索资源

//1.
//低级方法是检索封装数据的StreamResourceInfo对象，然后决定如何使用该对象。可通过代码，使用静态方法Application.GetResourceStream()完成该工作
//StreamResourceInfo sri = Application.GetResourceStream(new Uri("images/winter.jpg", UriKind.Relative));
//从StreamResourceInfo对象可以得到两部分信息：
//a.ContentType属性    返回一个描述数据类型的字符串(本例是image/jpg)
//b.Stream属性         返回一个UnmanagedMemoryStream对象，可使用该对象读取数据，一次读取一个字节
//(GetResourceStream()辅助方法封装了ResourceManager类和ResourceSet类)

//2.
//具体访问AssemblyName.g.resources资源流(这是存储所有WPF资源的地方)，并查找所需的对象
//Assembly assembly = Assembly.GetAssembly(this.GetType());
//string resourceName = assembly.GetName().Name + ".g";
//ResourceManager rm = new ResourceManager(resourceName, assembly);
//using(ResourceSet set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true))
//{
//  UnmanagedMemoryStream s;
//  //The second parameter (true) performs a case-insensitive resource lookup.
//  s = (UnmanagedMemoryStream)set.GetObject("images/winter.jpg", true);
//  ...
//}

//通过ResourceManager类和ResourceSet类还可以完成其他一些Application类自身不能完成的工作
//(在AssemblyName.g.resources资源流中所有嵌入资源的名称：)
//Assembly assembly = Assembly.GetAssembly(this.GetType());
//string resourceName = assembly.GetName().Name + ".g";
//ResourceManager rm = new ResourceManager(resourceName, assembly);
//using(ResourceSet set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true))
//{
//  foreach(DictionaryEntry res in set)
//  {
//      MessageBox.Show(res.Key.ToString());
//  }
//}

//3.使用能够理解资源的类
//之前的UnmanagedMemoryStream对象相对低级，对象本身没有什么用处，需要将它转换成一些更有意义的数据，例如具有属性和方法的高级对象
//WPF提供了几个专门使用资源的类。这些类不要求提取资源(这非常混乱且不是类型安全的)，他们使用资源的名称访问资源
//<Image Source="Images/Blue hills.jpg"/>   (注意这里使用的是正斜杠，因为这是WPF使用URI的约定(实际上这两种方式都行))
//img.Source = new BitmapImage(new Uri("images/winter.jpg", UriKind.Relative));











////内容文件

//当嵌入文件作为资源时，会将文件放到编译过的程序集中，并且可以确保文件总是可用的。对于部署而言这是理想选择，并且可避免可能存在的问题。
//然而在有些情况下，使用这种方法并不方便：
//1.希望改变资源文件，又不想重新编译应用程序
//2.资源文件非常大
//3.资源文件是可选的，并且可以不随程序集一起部署
//4.资源时声音文件
//(WPF声音类不支持程序集资源。因此，无法从资源流中析取音频文件并播放他们——至少，如果没有首先保存音频文件，就不能播放他们。这一局限是
//由于这些类使用的技术基础(Win32API和媒体播放器)造成的)

//显然，可使用应用程序部署文件，并为应用程序添加代码，进而从硬盘驱动器中读取这些文件来解决该问题。然而，WPF还有更方便地选择，使这一过程
//更加容易管理，可将这些未编译的文件标记为内容文件。

//不能将内容文件嵌入到程序集中。然而，WPF为程序集添加了AssemblyAssociatedContentFile特性，公告每个内容文件的存在。该特性还记录了每个内容文件
//相对于可执行文件的位置。最方便的是，当为能够理解资源的元素(如Image类)使用内容文件时，可使用相同的URI系统

//添加内容文件方式：
//为项目添加声明文件，在Solution Explorer中选择该文件，并在Properties窗口中将Build Action属性改为Content。确保将Copy to Output Directory属性
//设置为Copy Always，以保证当生成项目时将声音文件复制到输出目录中。
//然后就可以使用他:
//<MediaElement Name="Sound" Source="Sounds/start.wav" LoadedBehavior="Manual"/>

namespace WPF_AssemblyResources
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
