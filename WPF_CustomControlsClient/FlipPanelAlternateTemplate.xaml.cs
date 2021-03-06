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

namespace WPF_CustomControlsClient
{
    /// <summary>
    /// FlipPanelAlternateTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class FlipPanelAlternateTemplate : Window
    {
        public FlipPanelAlternateTemplate()
        {
            InitializeComponent();
        }

        private void cmdFlip_Click(object sender, RoutedEventArgs e)
        {
            panel.IsFlipped = !panel.IsFlipped;
        }
    }
}
