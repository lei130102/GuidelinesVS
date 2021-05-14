using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;//本例使用到他
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WPF_CodeMethodCodeAndXAML
{
    public class MyWindow : Window
    {
        private Button button1;

        public MyWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //COnfigure the form.
            this.Width = this.Height = 285;
            this.Left = this.Top = 100;
            this.Title = "Dynamically Loaded XAML";

            //Get the XAML content from an external file.
            DependencyObject rootElement = ReadXAML("DockPanel.xaml");

            //Insert the markup into this window.
            this.Content = rootElement;

            //Find the control with the appropriate name.
            //方法一：
            button1 = (Button)LogicalTreeHelper.FindLogicalNode(rootElement, "button1");
            //方法二：
            //根元素是DockPanel对象，DockPanel类继承自FrameworkElement类，这意味着
            //FrameworkElement frameworkElement = (FrameworkElement)rootElement;
            //button1 = (Button)frameworkElement.FindName("button1");

            //Wire up the event handler.
            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Content = "Thank you.";
        }

        DependencyObject ReadXAML(string xamlFile)
        {
            //DependencyObject是所有WPF控件继承的基类
            //DependencyObject对象可放在任意类型的容器中(如面板)，但在这个示例中他被用作整个窗口的内容
            DependencyObject obj;
            using (FileStream fs = new FileStream(xamlFile, FileMode.Open))
            {
                obj = (DependencyObject)XamlReader.Load(fs);
            }
            return obj;
        }
    }
}
