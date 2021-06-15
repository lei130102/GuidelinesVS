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

//绑定到对象集合

//绑定到单个对象是非常直观的。但当需要绑定到对象的某些集合时——比如数据表中的所有产品，问题会变得更有趣

//到目前为止介绍的每个依赖项属性都支持单值绑定，但集合绑定需要智能程度更高的元素。在WPF中，所有派生自ItemsControl的类都能显示条目的完整列表。能支持集合
//数据绑定的元素包括ListBox、ComboBox、ListView和DataGrid(以及用于显示层次化数据的Menu和TreeView)

//提示：尽管WPF看起来好像只提供了少数几个列表控件，但实际上可使用这些控件以近乎无限的方式显示数据。这是因为列表控件支持数据模板，通过数据模板可以完全控制
//数据项的显示方式

//为支持集合绑定，ItemsControl类定义了下面列出的三个重要属性
//ItemsSource               指向的集合包含将在列表中显示的所有对象
//DisplayMemberPath         确定用于为每个项创建显示文本的属性
//ItemTemplate              接受的数据模板用于为每个项创建可视化外观。这个属性比DisplayMemberPath属性的功能强大得多

//现在，您可能想了解什么类型的集合可用来填充ItemsSource属性。幸运的是，可使用任何内容。唯一的要求是支持IEnumerable接口，数组、各种类型的集合以及许多更
//特殊的封装了数据项组的对象都支持该接口。然而，基本的IEnumerable接口仅支持只读绑定。如果希望编辑集合(例如，希望插入和删除元素)，就需要更复杂的基本结构






//为创建本例，首先需要构建数据访问逻辑。在本例中，StoreDB.GetProducts()方法使用GetProducts存储过程检索数据库中所有产品的列表。为每条记录创建
//一个Product对象，并将创建的Product对象添加到List泛型集合中(在此可使用任何集合——例如数组或等效的弱类型的ArrayList)

//当单击Get Products按钮时，事件处理代码调用GetProducts()方法并将返回结果作为列表的ItemsSource属性的值

namespace WPF_DataBinding
{
    /// <summary>
    /// BindToCollection.xaml 的交互逻辑
    /// </summary>
    public partial class BindToCollection : Window
    {
        public BindToCollection()
        {
            InitializeComponent();
        }

        //为便于在代码中进行访问，还将该集合保存为窗口类的成员变量
        private ICollection<Product> products;


        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;

            //上面的代码会成功地使用Product对象填充列表。因为列表不知道如何显示产品对象，所以只是调用ToString()方法。
            //因为Product类没有重写该方法，所以只为每个项显示完全限定的类名
            //可以通过三种方法来解决这个问题：
            //1.设置列表的DisplayMemberPath属性
            //例如将该属性设置为ModelName
            //2.重写ToString()方法，返回更有用的信息
            //例如，可为每个项返回包含型号和型号名称的字符串。可以通过这种方法显示比列表中的一个属性更多的信息(例如，在Customer类中组合
            //FirstName和SecondName属性是非常合适的)。然而，仍不能对数据的显示进行更多控制
            //3.提供数据模板
            //可使用这种方法显示属性值的任何排列(并同时显示固定的文本)
        }

        //插入和移除集合项

        //本例的一个限制是不能获取对集合进行的修改。虽然该例注意到了发生变化的Product对象，但如果通过代码添加新项或删除项时不会更新列表
        //例如，添加Delete按钮，该按钮执行下面的代码
        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Remove((Product)lstProducts.SelectedItem);
        }
        //虽然删除的项已经从集合中移除了，但仍保留在绑定列表中
        //为启用集合更改跟踪，需要使用实现了INotifyCollectionChanged接口的集合。大多数通用集合没有实现该接口，包括List集合。实际上，
        //WPF提供了一个使用INotifyCollectionChanged接口的集合：ObservableCollection类

        //注意：如果有一个从Windows窗体导入的对象模型，可使用Windows窗体中与ObservableCollection类等效的BindingList集合。BindingList集合实现了
        //IBindingList接口而不是INotifyCollectionChanged接口，该接口包含的ListChanged事件与INotifyCollectionChanged.CollectionChanged事件扮演
        //相同的角色

        //可通过从ObservableCollection类继承自定义集合来自定义工作方式，但这不是必需的。在当前示例中，使用ObservableCollection<Product>集合代替
        //List<Product>对象即可满足要求
        //public List<Product> GetProducts()
        //{
        //    //SqlConnection con = new SqlConnection(connectionString);
        //    //SqlCommand cmd = new SqlCommand("GetProducts", con);
        //    //cmd.CommandType = CommandType.StoredProcedure;

        //    ObservableCollection<Product> products = new ObservableCollection<Product>();
        //    //...
        //}
        //GetProducts()方法的返回类型可以是List<Product>，因为ObservableCollection类继承自List类。为使该例更加通用，可为返回类型使用ICollection<Product>，因为
        //ICollection接口包含了需要使用的所有成员

        //现在，如果通过代码删除或添加项，列表就会相应地进行刷新。当然，仍由您负责创建在集合修改前执行的数据访问代码——例如从后台数据库中删除产品记录的代码

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
