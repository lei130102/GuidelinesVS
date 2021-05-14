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

namespace WPF_Style
{
    /// <summary>
    /// EventSetter.xaml 的交互逻辑
    /// </summary>
    public partial class EventSetter : Window
    {
        public EventSetter()
        {
            InitializeComponent();
        }

        private void element_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
        }

        private void element_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = null;
        }
    }
}
