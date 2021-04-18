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

//Opacity属性使元素的所有内容都是部分透明的。OpacityMask属性提供了更大的灵活性。可使元素的特定区域透明或者部分透明

//如果使用SolidColorBrush画刷为OpacityMask属性设置透明颜色，整个元素就会消失，如果使用SolidColorBrush画刷设置非透明颜色，元素将保持完全可见。颜色的其他细节(红、绿和蓝成分)并不重要
//当设置OpacityMask属性时会忽略他们

//使用SolidColorBrush画刷设置OpacityMask属性没什么意义，因为可使用Opacity属性更容易地实现相同的效果。然而，当使用更特殊的画刷类型时，例如使用LinearGradient或RadialGradientBrush画刷，
//OpacityMask属性就变得更有用了

namespace WPF_Shape.View
{
    /// <summary>
    /// OpacityMask.xaml 的交互逻辑
    /// </summary>
    public partial class OpacityMask : Window
    {
        public OpacityMask()
        {
            InitializeComponent();
        }
    }
}
