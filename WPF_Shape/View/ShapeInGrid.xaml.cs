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

//Grid填满了整个窗口，Grid包含了一个按比例改变尺寸的行，该行填满了整个Grid，最后椭圆填满了该行

//改变形状尺寸的行为依赖于Stretch属性的值，默认为Fill，相当于将HorizontalAlignment和VerticalAlignment属性设置为Stretch
//如果设置固定的宽度和高度，那么会忽略HorizontalAlignment和VerticalAlignment属性，但Stretch属性仍然起作用——决定如何在给定的范围内改变形状内容的尺寸

namespace WPF_Shape.View
{
    /// <summary>
    /// ShapeInGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeInGrid : Window
    {
        public ShapeInGrid()
        {
            InitializeComponent();
        }
    }
}
