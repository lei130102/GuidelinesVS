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

//如果形状(Shape)未提供Height和Width属性，形状(Shape)会根据他们的容器来设置自身的尺寸

//形状(Shape)在StackPanel容器中：
//形状(Shape)会缩小到看不见，因为StackPanel为了适应其内容改变了尺寸
//所以形状(Shape)不适合放在StackPanel中，同理还有DockPanel、WrapPanel

namespace WPF_Shape.View
{
    /// <summary>
    /// ShapeInStackPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeInStackPanel : Window
    {
        public ShapeInStackPanel()
        {
            InitializeComponent();
        }
    }
}
