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

//画刷填充区域，不管是元素的背景色(Background)、前景色(Foreground)以及边框(BorderBrush)，还是形状的内部填充(Fill)和笔画(Stroke)

//画刷特点:
//1.画刷支持更改通知，因为他们继承自Freezable类。因此，如果改变了画刷，任何使用画刷的元素都会自动重新绘制自身
//2.画刷支持部分透明。为此，只需要修改Opacity属性，使背景能够透过前面的内容进行显示
//3.通过SystemBrushes类可以访问这样的画刷：此类画刷使用Windows系统设置为当前计算机定义的首选颜色

namespace WPF_Shape.View
{
    /// <summary>
    /// SolidColorBrush.xaml 的交互逻辑
    /// </summary>
    public partial class SolidColorBrush : Window
    {
        public SolidColorBrush()
        {
            InitializeComponent();

            //使用命名颜色创建SolidColorBrush画刷
            this.button1.Foreground = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);

            //使用系统颜色创建SolidColorBrush画刷
            this.button1.Background = SystemColors.ControlBrush;

            //使用颜色创建SolidColorBrush画刷
            byte red = 0;
            byte green = 255;
            byte blue = 0;
            this.button1.BorderBrush = new System.Windows.Media.SolidColorBrush(Color.FromRgb(red, green, blue));
        }
    }
}
