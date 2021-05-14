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

namespace WPF_TabControl
{
    /// <summary>
    /// Control.xaml 的交互逻辑
    /// </summary>
    public partial class Control : Window
    {
        public Control()
        {
            InitializeComponent();
        }

        private void btnPreviousTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tcSample.SelectedIndex - 1;
            if(newIndex < 0)
            {
                newIndex = tcSample.Items.Count - 1;
            }
            tcSample.SelectedIndex = newIndex;
        }

        private void btnNextTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tcSample.SelectedIndex + 1;
            if(newIndex >= tcSample.Items.Count)
            {
                newIndex = 0;
            }
            tcSample.SelectedIndex = newIndex;
        }

        private void btnSelectedTab_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Selected tab:" + (tcSample.SelectedItem as TabItem).Header);
        }
    }
}

//控制TabControl
//有时您可能想要以编程方式选择控制哪个选项卡，或者获得所选选项卡的一些信息。WPF TabControl控件有几个属性可以实现上述需求，
//例如SelectedIndex和SelectedItem。在下一个示例中，我在第一个示例中添加了几个按钮，使得我们控制TabControl：

//如您所见，我简单地在界面的下半部分添加了一组按钮。前两个允许选择当前控件的上一个或下一个选项卡，而最后一个选项卡将显
//示有关当前所选选项卡的信息，如屏幕截图所示。

//前两个按钮使用SelectedIndex属性来确定位置，然后减去或添加一个值，确保新索引不为负且不高于可用项的总数。 第三个
//按钮使用SelectedItem属性来获取对所选选项卡的引用。 正如您所看到的，我必须将其类型转换为TabItem类以获取header属性，
//因为SelectedProperty默认是object类型。
