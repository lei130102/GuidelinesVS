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
    public partial class SelectedIndexSelectedItem : Window
    {
        public SelectedIndexSelectedItem()
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
            //必须将其类型转换为TabItem类以获取Header属性，因为SelectedProperty默认是object类型。 
            MessageBox.Show("Selected tab:" + (tcSample.SelectedItem as TabItem).Header);
        }
    }
}

