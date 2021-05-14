using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_TreeView.HDT;

//在XAML结构化文件中，可以看到TreeView的ItemTemplate具有一个HierarchicalDataTemplate。通过设置这个模板的ItemsSource属性，
//我指示它使用Items属性来查找子元素，并且我在内部定义了一个真正的模板，该模板只包含一个绑定到Title属性的TextBlock。

namespace WPF_TreeView
{
    namespace HDT
    {
        //对应XAML中的Item
        public class Item
        {
            public Item()
            {
                this.Children = new ObservableCollection<Item>();
            }

            public string Title { get; set; }

            //对应XAML中的Items
            public ObservableCollection<Item> Children { get; set; }
        }
    }

    /// <summary>
    /// HierarchicalDataTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class HierarchicalDataTemplate : Window
    {
        public HierarchicalDataTemplate()
        {
            InitializeComponent();

            Item root = new Item() { Title = "Menu" };
            Item childItem1 = new Item() { Title = "Child item #1" };
            childItem1.Children.Add(new Item() { Title = "Child item #1.1" });
            childItem1.Children.Add(new Item() { Title = "Child item #1.2" });
            root.Children.Add(childItem1);
            root.Children.Add(new Item() { Title = "Child item #2" });
            trvMenu.Items.Add(root);
        }
    }
}
