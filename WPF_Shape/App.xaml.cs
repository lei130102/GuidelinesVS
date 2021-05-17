using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//在许多用户界面技术中，普通控件和自定义绘图之间具有明显区别。通常来说，绘图特性只用于特定的应用程序——例如游戏、数据可视化和物理仿真等。

//WPF具有一个非常不同的原则。他以相同方式处理预先构建的控件和自定义绘制的图形。不仅可使用WPF的绘图支持为用户界面创建富图形的可视化元素，还可通过他最大限度地利用
//动画和控件模板等特性。实际上，无论是正在创建绚丽的新游戏还是仅对普通业务应用程序进行润色，WPF的绘图支持都是同等重要的

//在WPF用户界面中，绘制2D图形内容的最简单方法是使用形状(shape)——专门用于表示简单的直线、椭圆、矩形以及多边形的一些类。从技术角度看，形状就是所谓的绘图图元(primitive)。
//可组合这些基本元素来创建更复杂的图形

//形状(shape)继承自FrameworkElement类，因此形状都是元素，这样会带来许多重要的结果：
//1.形状绘制自身
//不需要管理无效的情况和绘图过程。例如，当移动内容、改变窗口尺寸或改变形状属性时，不需要手动重新绘制形状
//2.使用与其他元素相同的方式组织形状
//可在任何布局容器中放置形状(尽管Canvas明显是最有用的容器，因为他允许在特定的坐标位置放置形状，当构建复杂的具有多个部分的图画时，这很重要)
//3.形状支持与其他元素相同的事件
//这意味着为了处理焦点、按下键盘、移动鼠标以及单击鼠标等，不必执行任何额外工作。可使用用于其他元素的相同事件集，并同样支持工具提示、上下文菜单和拖放操作

//在WPF中仍可使用可视化层(visual layer)在更低层次上进行编程。如果需要创建非常多的元素(例如几千个形状)，并且不需要UIElement和FrameworkElement类提供的任何特性(如数据绑定和事件处理)，
//使用这个轻量级的模型可提高性能

//每个形状都继承自抽象类System.Windows.Shapes.Shape

//  DispatcherObject
//         |
//  DependencyObject
//         |
//      Visual
//         |
//    UIElement
//         |
//   FrameworkElement
//         |
//       Shape
//         |
//  Rectangle   Ellipse  Line    Polyline    Polygon    Path






namespace WPF_Shape
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
