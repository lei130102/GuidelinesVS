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

//可以采用以下几种方式使元素具有半透明效果：
//1.设置元素的Opacity属性
//每个元素(包括形状)都从UIElement基类继承了Opacity属性。不透明度(Opacity)是0到1之间的小数，1表示完全不透明(默认值)，0表示完全透明
//2.设置画刷的Opacity属性
//每个画刷也从Brush基类继承了Opacity属性
//3.使用具有透明Alpha值的颜色
//所有alpha值小于255的颜色都是半透明的

namespace WPF_Shape.View
{
    /// <summary>
    /// Opacity.xaml 的交互逻辑
    /// </summary>
    public partial class Opacity : Window
    {
        public Opacity()
        {
            InitializeComponent();
        }
    }
}
