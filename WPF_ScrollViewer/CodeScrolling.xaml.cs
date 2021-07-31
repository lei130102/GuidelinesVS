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
using System.Windows.Navigation;
using System.Windows.Shapes;

//如果希望让大量内容适应有限的空间，滚动是重要特性之一。在WPF中为了获得滚动支持，需要在ScrollViewer控件中封装希望滚动的内容

namespace WPF_ScrollViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CodeScrolling : Window
    {
        public CodeScrolling()
        {
            InitializeComponent();
        }

        private void LineUp_Click(object sender, RoutedEventArgs e)
        {
            scroller.LineUp();
        }

        private void LineDown_Click(object sender, RoutedEventArgs e)
        {
            scroller.LineDown();
        }

        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            scroller.PageUp();
        }

        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            scroller.PageDown();
        }
    }
}
