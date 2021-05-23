using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WPF_ControlTemplateBrowser
{
    public class TypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Type controlType = typeof(Control);
            List<Type> derivedTypes = new List<Type>();

            // Search all the types in the assembly where the Control class is defined.
            Assembly assembly = Assembly.GetAssembly(typeof(Control));
            foreach (Type type in assembly.GetTypes())
            {
                // Only add a type of the list if it's a Control, a concrete class, and public.
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }

            // Sort the types by type name.
            derivedTypes.Sort(new TypeComparer());

            // Show the list of types.
            lstTypes.ItemsSource = derivedTypes;
        }

        private void lstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected type.
                Type type = (Type)lstTypes.SelectedItem;

                // Instantiate the type.
                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                Window win = control as Window;
                if (win != null)
                {
                    // Create the window (but keep it minimized).
                    win.WindowState = System.Windows.WindowState.Minimized;
                    win.ShowInTaskbar = false;
                    win.Show();
                }
                else
                {
                    // Add it to the grid (but keep it hidden).
                    control.Visibility = Visibility.Collapsed;//使控件不可见
                    grid.Children.Add(control);
                }

                // Get the template.
                ControlTemplate template = control.Template;

                //将ControlTemplate对象转为更熟悉的XAML标记
                //使用XamlWriter和XamlWriterSettings对象以确保XAML缩进合理，便于阅读
                // Get the XAML for the template.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // Display the template.
                txtTemplate.Text = sb.ToString();

                // Remove the control from the grid.
                if (win != null)
                {
                    win.Close();
                }
                else
                {
                    grid.Children.Remove(control);
                }

                //所有这些代码都被封装在异常处理块中，异常处理块监视不能被创建或不能添加到Grid网格(如另一个Window或Page)中的控件产生的问题
            }
            catch (Exception err)
            {
                txtTemplate.Text = "<< Error generating template: " + err.Message + ">>";
            }
        }
    }

    //扩展该应用程序，从而在文本框中编辑模板，使用XamlReader将模板转换回ControlTemplate对象，然后指定给某个控件并观察效果，这并不是很困难。然而，
    //通过将模板放置到真实窗口中进行实际操作，测试和改进他们会更加容易

    //提示：如果正在使用Expression Blend，那么还可使用一个方便的特性为任何使用的控件编辑模板(从技术角度看，该步骤获取默认模板并为控件创建默认模板的一个
    //副本，然后编辑该副本)
    //为测试该特性，在设计视图中右击控件，然后选择Edit Control Part(Template) | Edit a Copy。控件模板的副班会被保存为资源，从而会提示您选择一个描述性
    //的资源键，并且还需要选择是在当前窗口还是在应用程序的全局资源中存储资源。如果选择后者，就可以在整个应用程序中使用控件模板
}
