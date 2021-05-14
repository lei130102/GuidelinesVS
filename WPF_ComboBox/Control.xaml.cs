using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WPF_ComboBox
{
    /// <summary>
    /// Control.xaml 的交互逻辑
    /// </summary>
    public partial class Control : Window
    {
        public Control()
        {
            InitializeComponent();

            cmbColors.ItemsSource = typeof(Colors).GetProperties();

            //使用反射Colors类的方法和获得所有颜色的列表。 我将它分配给ComboBox的ItemsSource属性，然后使用我在XAML部分中定义的模板呈现每个颜色。
            //ItemTemplate定义的每个项都包含一个带有Rectangle和TextBlock的StackPanel，每个项都绑定到颜色值。 这给了我们一个完整的颜色列表，只需要很少的工作量
        }

        //这个例子的有趣部分是三个按钮的事件处理程序，以及SelectionChanged事件处理程序。 前两个按钮中，我们通过读取SelectedIndex
        //属性然后减去或加上一来选择上一个或下一个项。 非常简单易用。

        //在第三个事件处理程序中，我们使用SelectedItem根据值选择特定项。 我在这里做了一些额外的工作（使用.NET反射），因为ComboBox
        //被绑定到一个属性列表，每个属性都是一种颜色，而不是一个简单的颜色列表，基本上就是将一个项包含的值赋予SelectedItem属性。

        //在第四个和最后一个事件处理程序中，我响应所选项的变化。 变化时，我会读取所选颜色（再次使用反射，如上所述），然后使用所选颜
        //色为窗口创建新的背景画笔。 可以在屏幕截图中看到效果。

        //如果您使用可编辑的ComboBox（IsEditable属性设置为true），可以读取Text属性以了解用户输入或选择的值。

        private void cmbColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
            this.Background = new SolidColorBrush(selectedColor);
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(cmbColors.SelectedIndex > 0)
            {
                cmbColors.SelectedIndex = cmbColors.SelectedIndex - 1;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(cmbColors.SelectedIndex < cmbColors.Items.Count - 1)
            {
                cmbColors.SelectedIndex = cmbColors.SelectedIndex + 1;
            }
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            cmbColors.SelectedItem = typeof(Colors).GetProperty("Blue");
        }
    }
}
