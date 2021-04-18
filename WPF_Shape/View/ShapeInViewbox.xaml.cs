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
using System.Windows.Shapes;

//Viewbox是继承自Decorator的简单类。该类只接受一个子元素，并拉伸或缩小子元素以适应可用的空间。他更常用于矢量图像而不是普通控件。通常在Viewbox控件中放置Canvas，并在Canvas中放置形状

//注意：
//Viewbox元素执行的缩放和在WPF中当增加系统DPI设置时看到的缩放类似。他按比例改变屏幕上的每个元素，包括图像、文本、直线以及形状。例如，如果在Viewbox元素中放置一个普通按钮，尺寸的改变会
//影响他的整个尺寸、内部的文本以及周围边框的粗细。如果在Viewbox元素内部放置一个形状元素，他会按比例地改变形状内部的区域和边框，从而当放大形状时，其边框也将变粗

namespace WPF_Shape.View
{
    /// <summary>
    /// ShapeInViewbox.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeInViewbox : Window
    {
        public ShapeInViewbox()
        {
            InitializeComponent();
        }
    }
}
