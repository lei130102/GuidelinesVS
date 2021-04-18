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

//RenderTransform和RenderTransformOrigin属性并不限制只能用于形状。实际上，Shape类的这些属性从UIElement类继承而来，这意味着所有WPF元素都支持这两个属性

//RenderTransform不是在WPF基类中定义的唯一与变换相关的属性。FrameworkElement类还定义了LayoutTransform属性。LayoutTransform属性以相同的方式变换元素，
//但在布局之前执行其工作。这种情况的开销虽然更大些，但如果使用布局容器为一组控件提供自动布局功能，这种方式是很关键的(Shape类也提供了LayoutTransform属性，
//但很少需要使用该属性，因为通常使用容器(如Canvas)明确地放置形状，而不是使用自动布局)

//只有很少几个元素不能被变换，因为他们的呈现工作并非由WPF本身负责。不能被变换的元素的两个例子是WindowsFormHost和WebBrower元素。
//WindowsFormHost元素用于在WPF窗口中放置Windows窗体控件
//WebBrower元素用于显示HTML内容

namespace WPF_Shape.View
{
    /// <summary>
    /// RotateTransform.xaml 的交互逻辑
    /// </summary>
    public partial class RotateTransform : Window
    {
        public RotateTransform()
        {
            InitializeComponent();
        }
    }
}
