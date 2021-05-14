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

namespace WPF_WindowTracker
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

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            //还设置了Window.Owner属性，使所有文档窗口都在创建他们的主窗口上显示
            doc.Owner = this;
            doc.Show();
            ((App)Application.Current).Documents.Add(doc);

            //也可在Document类中响应Window.Loaded这类事件，以确保当创建文档对象时，总会在Documents集合中注册该文档对象
        }

        private void cmdUpdate_Click(object sender, RoutedEventArgs e)
        {
            foreach(Document doc in ((App)Application.Current).Documents)
            {
                doc.SetContent("Refreshed at " + DateTime.Now.ToLongTimeString() + ".");
            }
        }
    }
}
