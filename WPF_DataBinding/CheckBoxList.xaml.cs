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
    /// CheckBoxList.xaml 的交互逻辑
    /// </summary>
    public partial class CheckBoxList : Window
    {
        public CheckBoxList()
        {
            InitializeComponent();

            lstProducts.ItemsSource = App.StoreDb.GetProducts();
        }

        private void cmdGetSelectedItems_Click(object sender, RoutedEventArgs e)
        {
            if(lstProducts.SelectedItems.Count > 0)
            {
                string items = "You selected: ";
                foreach(Product product in lstProducts.SelectedItems)
                {
                    items += "\n * " + product.ModelName;
                }
                MessageBox.Show(items);
            }
        }
    }
}
