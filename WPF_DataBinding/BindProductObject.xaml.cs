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
    /// BindProductObject.xaml 的交互逻辑
    /// </summary>
    public partial class BindProductObject : Window
    {
        public BindProductObject()
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
                    gridProductDetails.DataContext = App.StoreDb.GetProduct(ID);
                }
                catch
                {
                    MessageBox.Show("Error contacting database.");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }
        }
    }
}
