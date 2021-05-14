using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

//在Application_Startup事件期间或最近在主窗口的构造函数中更改区域性最有意义，因为在更改CurrentCulture属性时，已生成的值不会自动更新。
//这并不意味着你不能这样做，如下一个例子所示，它也可以很好地演示输出如何受CurrentCulture属性的影响：

namespace WPF_Culture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo((sender as Button).Tag.ToString());
            lblNumber.Content = (123456789.42d).ToString("N2");
            lblDate.Content = DateTime.Now.ToString();
        }
    }
}

//如果您的应用程序使用多个线程，则应考虑使用DefaultThreadCurrentCulture属性。 它可以在CultureInfo类（在.NET框架版本4.5中引入）中找到，
//并且将确保不仅当前线程，而且未来线程将使用相同的区域性。 您可以像这样使用它，例如 在Application_Startup事件中：

//CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
//那么，您是否必须同时设置CurrentCulture和DefaultThreadCurrentCulture属性？ 实际上，不 - 如果您尚未更改CurrentCulture属性，则设置
//DefaultThreadCurrentCulture属性也将应用于CurrentCulture属性。 换句话说，如果您计划在应用程序中使用多个线程，则使用DefaultThreadCurrentCulture
//而不是CurrentCulture是有意义的 - 它将负责所有场景。
