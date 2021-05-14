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
using WPF_ListView.GV;


//在之前的ListView文章中，我们使用了WPF ListView的最基本版本，它是没有指定自定义View的版本。这导致ListView的行为与WPF ListBox非常相似，但有一些细微差别。真正的区别在于视图，而WPF内置了一个专门的视图：GridView。

//通过使用GridView，您可以在ListView中获得多列数据，就像您在Windows资源管理器中看到的那样



//在标记（XAML）中，我们使用ListView.View属性为ListView定义View。我们将它设置为GridView，这是目前WPF中唯一包含的视图类型（您可以轻松创建自己的视图类型！）。 GridView为我们提供了您在屏幕截图中看到的基于列的视图。

//在GridView内部，我们定义了三列，一列用于我们希望显示的每个数据。 Header属性用于指定我们要为列显示的文本，然后我们使用DisplayMemberBinding属性将值绑定到User类的属性。




//模板化单元格内容
//使用DisplayMemberBinding 属性几乎只限于输出简单的字符串，根本没有自定义格式，但WPF ListView比这更灵活。通过指定CellTemplate，我们可以完全控制内容在特定列单元格中的呈现方式。
//GridViewColumn将使用DisplayMemberBinding作为它的第一优先级。第二个选择是CellTemplate属性，我们将在这个例子中使用它：


//我们为最后一列指定了一个自定义CellTemplate，我们希望为电子邮件地址做一些特殊的格式化。对于我们只想要基本文本输出的其他列，我们坚持使用DisplayMemberBinding，因为它需要更少的标记。

namespace WPF_ListView
{
    namespace GV
    {
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Mail { get; set; }
        }
    }

    /// <summary>
    /// GridView.xaml 的交互逻辑
    /// </summary>
    public partial class GridView : Window
    {
        public GridView()
        {
            InitializeComponent();

            List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
            items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
            items.Add(new User() { Name = "Sammy Doe", Age = 7, Mail = "sammy.doe@gmail.com" });
            lvUsers.ItemsSource = items;
        }
    }
}
