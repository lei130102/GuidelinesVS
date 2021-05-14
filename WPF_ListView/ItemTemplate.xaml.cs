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
using WPF_ListView.IT;

//WPF的工作实际上就是不断定义模板，所以你可以很容易地给ListView定义数据模板。在下面的例子中，我们会给每个项自定义格式，
//只为了向你展示这样做会使ListView多么的灵活多变。

//通过自定义ItemTemplate和数据绑定，我们可以构建非常漂亮的ListView控件。然而它还是看起来很像一个ListBox。一个ListView
//的非常常见的使用场景是添加列，有时(例如在WinForm应用中)它还被用来当作展示一些具体数据的视图。WPF有一个自带的视图类来处
//理这些场景，而这将在下一章节具体讨论。

namespace WPF_ListView
{
    namespace IT
    {
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Mail { get; set; }
        }
    }

    /// <summary>
    /// ItemTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class ItemTemplate : Window
    {
        public ItemTemplate()
        {
            InitializeComponent();

            List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
            items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe=family.com" });
            items.Add(new User() { Name = "Sammy Doe", Age = 13, Mail = "sammy.doe@gmail.com" });

            lvDataBinding.ItemsSource = items;
        }
    }
}
