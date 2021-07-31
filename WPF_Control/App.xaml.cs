using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//////控件——继承自System.Windows.Control类
//包括以下控件
//1.内容控件
//可包含嵌套的元素
//主要有Label Button ToolTip ScrollViewer
//2.带有标题的内容控件
//允许添加 主要内容部分 以及 单独标题部分 的内容控件
//主要有TabItem GroupBox Expander
//3.文本控件
//允许用户输入文本
//主要有TextBox PasswordBox RichTextBox
//4.列表控件
//在列表中显示项的集合
//主要有ListBox ComboBox
//5.基于范围的控件
//只有共同的属性Value，可使用预先规定范围内的任何数字设置该属性
//主要有Slider ProgressBar
//6.日期控件
//允许用户选择日期
//主要有Calendar DatePicker
//7.其他
//创建菜单、工具栏以及功能区的控件
//显示绑定数据的网格控件和树控件
//允许查看和编辑富文档的控件







//WPF窗口充满了各种元素，但这些元素中只有一部分是控件，控件通常被描述为与用户交互的元素——能接收焦点并接受键盘或鼠标输入的元素

//System.Windows.Control基类提供了一小部分基础功能：
//1.设置控件内容对齐方式的能力
//2.设置Tab键顺序的能力
//3.支持绘制背景、前景和边框
//3.1背景和前景
//所有控件都包含背景和前景的概念，通常，背景是控件的表面，而前景是文本。在WPF中，分别使用Background和Foreground属性设置这两个区域(但非内容)的颜色
//如果使用这种方法，就会发现当按钮处于正常状态时（未被按下）会为该按钮设置背景色，但当按下按钮时就不会改变按钮显示的颜色。为了真正地自定义按钮外观额每个方面，需要使用模板

//a.用代码设置颜色
//int red = 0; int green = 255; int blue = 0;
//cmd.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));//使用颜色值
//cmd.Background = new SolidColorBrush(Colors.AliceBlue);//使用颜色名
//cmd.Background = new SolidColorBrush(SystemColors.ControlColor);//使用系统画刷(注意是ControlColor)
//cmd.Background = SystemColors.ControlBrush;//使用系统画刷(注意是ControlBrush)
//(如果系统颜色在运行这段代码后发生了变化，不会使用新的颜色更新按钮。本质上，代码获取的是当前颜色或画刷的快照。为确保程序能够根据配置的变化进行更新，需要使用动态资源)

//b.使用xaml设置颜色
//<Button Background="Red">A Button</Button>
//或者
//<Button>
//    <Button.Background>
//        <SolidColorBrush Color="Red"/>
//    </Button.Background>
//</Button>
//或者
//<Button Background="#FFFF0000">A Button</Button>

//#rrggbb 或 #aarrggbb

//3.2边框颜色
//使用BorderBrush属性设置边框颜色
//(有些控件不支持BorderBrush和BorderThickness属性。Button对象就将完全忽略他们，因为Button对象使用ButtonChrome装饰元素定义自己的背景和边框，
//然而，可使用模板为按钮设置新的外观(使用所选的边框))

//(画刷支持自动更改通知。换句话说，如果将画刷关联到某个控件并改变画刷，控件将相应地更新自身。这之所以能够工作，是因为画刷继承自System.Windows.Freezable类，
//名称源于这样一个事实：所有可冻结的对象都有两种状态——可读状态和只读状态(或冻结状态))

//4.支持格式化文本内容的尺寸和字体
//见WPF_Font例子

//5.鼠标光标
//见WPF_Cursor例子



////分类
//1.内容控件                        可以包含嵌套的元素                                    Label、Button、ToolTip和ScrollViewer等
//2.带有标题和内容的控件             可以添加主要内容部分以及单独标题部分的内容控件           TabItem、GroupBox和Expander等
//3.文本控件                        允许用户输入文本                                      TextBox、PasswordBox和RichTextBox
//4.列表控件                        可以在列表中显示项的集合                               ListBox、ComboBox等
//5.基于范围的控件                   可以使用预先规定范围内的任何数字设置属性Value           Slider和ProgressBar等
//6.日期控件                        允许用户选择日期                                      Calendar和DatePicker等
//7.用于创建菜单、工具栏以及功能区的控件
//8.用于显示绑定数据的网格控件和树控件
//9.允许查看和编辑富文本的控件

namespace WPF_Control
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
