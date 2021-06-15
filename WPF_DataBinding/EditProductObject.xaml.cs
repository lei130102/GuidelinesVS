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

namespace WPF_DataBinding
{
    /// <summary>
    /// EditProductObject.xaml 的交互逻辑
    /// </summary>
    public partial class EditProductObject : Window
    {
        private Product product;

        public EditProductObject()
        {
            InitializeComponent();
        }

        private void cmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if(Int32.TryParse(txtID.Text, out ID))
            {
                try
                {
                    product = App.StoreDb.GetProduct(ID);
                    gridProductDetails.DataContext = product;
                }
                catch
                {
                    MessageBox.Show("Error contacting database");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }
        }

        //更新数据库

        //在本例中，如果想启用数据更新功能，不需要做任何额外的工作。TextBox.Text属性在默认情况下使用双向绑定，这意味着当在文本框中编辑文本时会修改
        //Product对象(从技术角度看，当使用Tab键将焦点转移到新的字段时，会更新每个属性，因为TextBox.Text属性默认的源更新模式是LostFocus)

        //可随时向数据库提交修改。需要做的全部工作是为StoreDB类添加UpdateProduct()方法，并为窗口添加Update按钮。当单击Update按钮时，代码从数据上下文
        //中获取当前Product对象，并使用该对象提交更新信息：
        //private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    Product product = (Product)gridProductDetails.DataContext;
        //    try
        //    {
        //        App.StoreDb.UpdateProduct(product);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Error contacting database.");
        //    }
        //}
        //这存在一个潜在问题。当单击Update按钮时，焦点会转移到该按钮上，任何尚未提交的编辑会应用于Product对象。但如果将Update按钮设置为默认按钮(通过将
        //IsDefault属性设置为true)，还有另一种可能。用户可修改其中一个字段并按回车键来触发更新过程，但不会提交最后的修改。为避免出现这种情况，在执行
        //任何数据库代码之前，可显式地强制转移焦点
        //FocusManager.SetFocusedElement(this, (Button)sender);

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);

            MessageBox.Show(product.UnitCost.ToString());
        }

        private void cmdIncreasePrice_Click(object sender, RoutedEventArgs e)
        {
            product.UnitCost *= 1.1M;
        }

    }
}
