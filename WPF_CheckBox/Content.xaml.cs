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

namespace WPF_CheckBox
{
    /// <summary>
    /// Content.xaml 的交互逻辑
    /// </summary>
    public partial class Content : Window
    {
        public Content()
        {
            InitializeComponent();
        }

        private void cbAllFeatures_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool newVal = (cbAllFeatures.IsChecked == true);
            cbFeatureAbc.IsChecked = newVal;
            cbFeatureXyz.IsChecked = newVal;
            cbFeatureWww.IsChecked = newVal;
        }

        private void cbFeature_CheckedChanged(object sender, RoutedEventArgs e)
        {
            cbAllFeatures.IsChecked = null;

            if((cbFeatureAbc.IsChecked == true) && (cbFeatureXyz.IsChecked == true) && (cbFeatureWww.IsChecked == true))
            {
                cbAllFeatures.IsChecked = true;
            }

            if((cbFeatureAbc.IsChecked == false) && (cbFeatureXyz.IsChecked == false) && (cbFeatureWww.IsChecked == false))
            {
                cbAllFeatures.IsChecked = false;
            }
        }
    }
}


//这个例子有两个角度：如果你勾选或取消“全部启用”的 CheckBox，则那些代表一个个功能的子 CheckBox 也会一起被勾选或取消。反过来看也成立：单独操作子 CheckBox 也会影响“全部启用” CheckBox 的状态。如果子 CheckBox 全部被勾选或取消，“全部启用” CheckBox 就会获得对应的状态。但要是子 CheckBox 的状态不统一，“全部启用” CheckBox 的值就会为空值，令它进入不定态。
//所有这些特性都在上方的截图里，通过订阅 CheckBox 控件的 Checked 和 Unchecked 事件来实现。在现实世界中，你可能会直接绑定这些值，但这个例子是一个最基本的、利用 IsThreeState 属性来创建“全部启用”效果的实现
