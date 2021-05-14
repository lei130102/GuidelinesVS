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

namespace WPF_Binding
{
    /// <summary>
    /// WindowDataContext.xaml 的交互逻辑
    /// </summary>
    public partial class WindowDataContext : Window
    {
        public WindowDataContext()
        {
            InitializeComponent();

            //下面代码不知道在XAML中等价实现方式
            this.DataContext = this;
        }
    }
}
