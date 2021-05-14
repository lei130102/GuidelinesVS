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

namespace WPF_Font
{
    /// <summary>
    /// EmbeddedFont.xaml 的交互逻辑
    /// </summary>
    public partial class EmbeddedFont : Window
    {
        public EmbeddedFont()
        {
            InitializeComponent();

            //MessageBox.Show(tst.FontFamily.BaseUri.ToString());
        }
    }
}
