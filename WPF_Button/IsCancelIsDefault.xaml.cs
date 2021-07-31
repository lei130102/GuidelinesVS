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


namespace WPF_Button
{
    /// <summary>
    /// IsCancelIsDefault.xaml 的交互逻辑
    /// </summary>
    public partial class IsCancelIsDefault : Window
    {
        public IsCancelIsDefault()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("IsCancel=True Button Clicked");
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("IsDefault=True Button Clicked");
        }
    }
}
