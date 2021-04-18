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

//形状(Shape)最理想的容器是Canvas，这样可以完全控制形状(Shape)如何相互重叠

//使用Canvas控件的唯一限制是图形(Shape)不能改变自身的尺寸以适应更大或更小的窗口，对于此类情况，WPF提供了简便的解决方法。如果希望联合Canvas控件的精确控制功能和方便的改变尺寸功能，可使用Viewbox元素

namespace WPF_Shape.View
{
    /// <summary>
    /// ShapeInCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeInCanvas : Window
    {
        public ShapeInCanvas()
        {
            InitializeComponent();
        }
    }
}
