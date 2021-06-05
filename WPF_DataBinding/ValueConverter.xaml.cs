using StoreDatabase;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    [ValueConversion(typeof(decimal), typeof(string))]
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            return price.ToString("c", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if(Decimal.TryParse(price, System.Globalization.NumberStyles.Any, culture, out result))
            {
                return result;
            }
            return value;
        }
    }

    /// <summary>
    /// ValueConverter.xaml 的交互逻辑
    /// </summary>
    public partial class ValueConverter : Window
    {
        private Product product;

        public ValueConverter()
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
                catch(Exception ex)
                {
                    MessageBox.Show("Error contacting database.");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);

            MessageBox.Show(product.UnitCost.ToString());
        }
    }
}
