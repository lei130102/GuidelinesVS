using StoreDatabase;
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

//绑定到LINQ表达式

//WPF支持LINQ(Language Integrated Query)语言，LINQ是用于查询各种数据源的通用语法，并与C#紧密集成。LINQ可使用具有LINQ提供者的任何数据源。通过
//.NET提供的支持，可使用类似的结构化的LINQ查询，从位于内存中的集合、XML文件或SQL Server数据库中检索数据。与其他查询语言一样，可使用LINQ对检索到的
//数据应用过滤、排列、分组以及变换

//尽管LINQ在一定程度上超出了本章的讨论范围，但可以通过一个简单的示例来学习许多内容。

//例如，设想有一个包含Product对象的集合products，并希望创建第二个集合，
//在该集合中只包含价格超过100美元的产品。如果使用过程代码，可编写如下内容：
//// Get the full list of products
//List<Product> products = App.StoreDB.GetProducts();
//
//// Create a second collection with matching products.
//List<Product> matches = new List<Product>();
//foreach(Product product in products)
//{
//  if(product.UnitCost >= 100)
//  {
//      matches.Add(product);
//  }
//
//}
//如果使用LINQ，就可以使用下面这个更加简洁的表达式：
//// Get the full list of products.
//List<Product> products = App.StoreDB.GetProducts();
//
//// Create a second collection with matching products.
//IEnumerable<Product> matches = from product in products where product.UnitCost >= 100 select product;
//这个示例为集合使用LINQ，这意味着使用LINQ表达式从位于内存中的集合查询数据。LINQ表达式使用一套新的语言关键字，包括from、in、
//where以及select。这些LINQ关键字是C#语言的一部分

//LINQ的基础是IEnumerable<T>接口。不管使用什么数据源，每个LINQ表达式都返回一些实现了IEnumerable<T>接口的对象。因为IEnumerable<T>接口扩展了
//IEnumerable接口，所以可将他绑定到WPF窗口，就像绑定普通集合那样：
//lstProducts.ItemsSource = matches;

//与ObservableCollection集合以及DataTable类不同，IEnumerable<T>接口没有提供添加和删除项的方法。如果需要使用这一功能，首先需要使用ToArray()
//或ToList()方法将IEnumerable<T>对象转换为数组或List集合

//下面的示例使用ToList()方法将前面显示的LINQ查询结果转换成强类型的Product对象的List集合：
//List<Product> productMatches = matches.ToList();

//注意：ToList()是扩展方法，这意味着定义他的类并不是使用他的类。从技术角度看，ToList()方法是在System.Linq.Enumerable辅助类中定义的，而且所有
//IEnumerable<T>对象都可以使用该方法。然而，如果Enumerable类超出了范围，就不能使用该方法，这意味着如果没有导入System.Linq命名空间，此处给出的
//代码就不能正常运行

//ToList()方法导致LINQ表达式被立即计算。最终结果是普通集合，可使用各种常见的方法处理普通集合。例如，可在ObservableCollection集合中封装普通集合
//以获得通知事件，从而使所有变化都能被立即反映到绑定的控件上：
//ObservableCollection<Product> productMatchesTracked = new ObservableCollection<Product>(productMatches);
//然后，可将productMatchesTracked集合绑定到窗口中的控件上。

namespace WPF_DataBinding
{
    /// <summary>
    /// BindToLinqFilteredCollection.xaml 的交互逻辑
    /// </summary>
    public partial class BindToLinqFilteredCollection : Window
    {
        public BindToLinqFilteredCollection()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProductsFilteredWithLinq(Decimal.Parse(txtMinimumCost.Text));
            lstProducts.ItemsSource = products;
        }

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Remove((Product)lstProducts.SelectedItem);
        }

        private void cmdAddProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Add(new Product("00000", "?", 0, "?"));
        }

        private bool isDirty = false;
        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDirty = true;
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isDirty = false;
        }
    }
}

//在Visual Studio中设计数据表单

//编写数据访问代码并填充几十个绑定表达式可能需要一些时间，并且如果创建使用数据库的多个WPF应用程序，您可能会发现在所有应用程序中编写了类似的代码和
//标记。这正是为什么Visual Studio能够生成数据访问代码并自动插入数据绑定控件的原因

//为使用这些功能，首先需要创建Visual Studio数据源(数据源是定义，通过数据源Visual Studio可以识别您的后台数据提供程序，并提供使用数据源的
//代码生成服务)。可创建封装了数据库、Web服务、已有数据访问类或SharePoint服务器的数据源。最常用的选择是创建实体数据模型(entity data model)，
//实体数据模型是一系列生成的类，这些类建立数据库中表的模型，并且可以用于查询数据表，在一定程度上与本章使用的数据访问组件类似。优点显而易见——使用
//实体数据模型可避免编写通常冗长乏味的数据代码。缺点也同样明显——如果希望数据逻辑准确地按照您的意图工作，需要花费一些时间修改选项，查找适当的可
//扩展点，并查看冗长的代码。如果希望调用特定的存储过程、缓存查询的结果、使用特定的并发策略或记录数据访问操作，那么可能希望进一步控制数据访问逻辑。
//通常可以使用实体数据模型实现这些技巧，但需要做更多的工作并且可能会抵消自动生成代码这一优点

//为创建数据源，选择Data | Add New Data Source 菜单项，启动Data Source Configuration Wizard，该向导会要求您选择数据源(本例中是一个数据库)，然后
//提示您选择附加信息(如希望查询的表和字段)。一旦添加数据源，就可以使用Data Sources窗口创建绑定控件。基本方法很简单。首先选择Data | Show Data Sources
//菜单项以查看Data Source窗口，该窗口列出了已选的表和字段。然后可从Data Sources窗格中向窗口的设计视图中拖动单个字段(以创建绑定的TextBlock、TextBox、
//ListBox或其他控件)，或者拖动整个数据表(以创建绑定的DataGrid或ListView)

//WPF的数据表单功能提供了用于构建数据驱动应用程序的快捷方法，但他们不知道接下来将实际如何操作。如果需要简单地查看或编辑数据，并且不希望耗费大量地时间来
//调整功能和用户界面，这些可能是正确地选择。他们通常适用于常规的商业应用程序
