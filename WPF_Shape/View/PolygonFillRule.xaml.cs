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

//每个Polygon和Polyline形状都有FillRule属性，该属性用于从两种填充方法中选择一种来填充区域

//FillRule默认为EvenOdd，为了确定是否填充区域，WPF计算为了到达形状的外部必须穿过的直线的数量。如果是奇数，就填充区域；如果是偶数，就不填充区域

namespace WPF_Shape.View
{
    /// <summary>
    /// PolygonFillRule.xaml 的交互逻辑
    /// </summary>
    public partial class PolygonFillRule : Window
    {
        public PolygonFillRule()
        {
            InitializeComponent();
        }
    }
}
