using System;
using System.Collections.Generic;
using System.Data;
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


//绑定到ADO.NET对象

//之前学过的自定义对象的所有功能，对于处理断开连接的ADO.NET数据对象同样有效
//在后台可以使用DataSet、DataTable以及DataRow对象，而不是使用自定义的Product类和ObservableCollection

//为测试这种情况，首先分析以下版本的GetProducts()方法，该方法提取相同的数据，但将数据打包到DataTable对象中
//public DataTable GetProducts()
//{
//    SqlConnection con = new SqlConnection(connectionString);
//
//    SqlCommand cmd = new SqlCommand("GetProducts", con);
//    cmd.CommandType = CommandType.StoredProcedure;
//
//    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//
//    DataSet ds = new DataSet();
//    adapter.Fill(ds, "Products");
//    return ds.Tables[0];
//}
//可检索DataTable对象并使用与绑定ObservableCollection集合几乎完全相同的方式将他绑定到列表上。唯一的区别是不能直接绑定到DataTable对象本身，而是需要
//通过众所周知的DataView对象，尽管可以手工创建DataView对象，但是每个DataTable对象都包含了一个已经准备好的DataView对象，可通过DataTable.DefaultView
//属性获取该对象

//注意：这并非什么新限制。即使在Windows窗口应用程序中，所有DataTable数据绑定也都是通过DataView进行的。区别是在Windows窗体中可以隐藏这一实际过程。他允许
//用户编写看似直接绑定到DataTable对象的代码，而当代码实际运行时使用的却是DataTable.DefaultView属性提供的DataView对象

namespace WPF_DataBinding
{
    /// <summary>
    /// BindToDataSet.xaml 的交互逻辑
    /// </summary>
    public partial class BindToDataSet : Window
    {
        public BindToDataSet()
        {
            InitializeComponent();
        }

        private DataTable products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDbDataSet.GetProducts();
            lstProducts.ItemsSource = products.DefaultView;
            //现在列表会为DataTable.Rows集合中的每个DataRow对象创建单独的项。为确定在列表中显示什么内容，需要使用希望显示的字段的名称
            //或使用数据模板设置DisplayMemberPath属性

            //这个示例好的一面在于，一旦改变提取数据的代码，就不需要执行任何其他修改。当在列表中选择一项时，下面的Grid控件会为他的数据上下文
            //提取所选的项。用于ProductList集合的标记仍能工作，因为Product类的属性名称和DataRow对象的字段名称是一致的

            //这个示例另一个好的方面在于，要实现更改通知，不必采取任何额外的步骤。这是因为DataView类实现了IBindingList接口，如果添加新的
            //DataRow对象或者删除已有的DataRow对象，DataView类会通知WPF基础结构
        }



        //然而，当删除DataRow对象时需要小心。可能会使用如下代码删除当前选择的记录：
        //products.Rows.Remove((DataRow)lstProducts.SelectedItem);
        //上面的代码存在两个方面的问题。首先，在列表中选择的项不是DataRow对象——而是由DataView对象提供的精简过的DataRowView封装器。其次，您可能
        //不希望从数据表的行集合中删除DataRow对象，而是希望将他标记为已经删除，从而当向数据库提交修改时，删除相应的记录

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            //下面是正确的代码，该代码获取选择的DataRowView对象，使用该对象的Row属性查找对应的DataRow对象，并调用找到的DataRow对象的Delete()方法
            //将行标识为即将删除：

            ((DataRowView)lstProducts.SelectedItem).Row.Delete();

            //现在，准备删除的DataRow对象从列表中消失了，但从技术上看他仍然位于DataTable.Rows集合中，这是因为DataView中的默认过滤设置隐藏了所有
            //已删除的记录
        }
    }
}
