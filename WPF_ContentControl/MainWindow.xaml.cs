using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//内容控件 和 布局容器

//内容控件只能包含一个子元素
//布局容器可以包含任意多个嵌套元素
//(仍可在单个内容控件中放置大量内容，比如使用布局容器作为子元素)

//内容控件都继承自抽象类ContentControl
//布局容器都继承自抽象类Panel

//DispatcherObject
//      |
//DependencyObject
//      |
//    Visual
//      |
//   UIElement
//      |
//FrameworkElement
//      |
//    Control
//      |
//  ContentControl

//常见的内容控件：
//Label ButtonBase ToolTip ScrollViewer UserControl Window HeaderedContentControl GroupBox TabItem Expander

//Control类提供Content属性
//Panel类提供Children集合

//Content属性支持任何类型对象，但可将该属性支持的对象分为两大类，针对每一类进行不同的处理：
//a.未继承自UIElement类的对象
//内容控件调用这些控件的ToString()方法获取文本，然后显示该文本
//b.继承自UIElement类的对象
//这些对象(包括所有可视化对象，他们是WPF的组成部分)使用UIElement.OnRender()方法在内容控件的内部进行显示
//(从技术角度看，OnRender()方法并不立即绘制对象——只是生成WPF在屏幕上绘图所需要的图形表示)

//可在内容控件中放置文本内容，但不能直接在布局容器中放置字符串内容(需要使用继承自UIElement的类对字符串进行封装，比如TextBlock或者Label类)

//有一个元素不允许放置到内容控件中，就是Window元素。当创建Window元素时，他会进行检查以确认他是否是顶级容器。如果被放入到另一个元素中，Window元素会抛出异常。

//除Content属性外，ContentControl类没有增加多少其他属性。
//HasContent属性
//如果在控件中有内容，该属性返回true。
//ContentTemplate属性
//通过该属性可创建一个模板，用于告诉控件如何显示他无法识别的对象。可更加智能地显示非继承自UIElement的对象，不是仅调用ToString()方法获取字符串，
//而是可以使用各种属性值，将他们布置到更复杂的标记中


//特殊容器
//内容控件还包含特殊容器，这些容器可用于构造用户界面中比较大的部分区域
//1.ScrollViewer控件
//2.HeaderedContentControl类(GroupBox控件、TabItem控件和Expander控件)
//表示包含单一元素内容(存储在Content属性中)和单一元素标题(存储在Header属性中)

namespace WPF_ContentControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
