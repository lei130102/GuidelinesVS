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
    public class NoBlankProductRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup bindingGroup = (BindingGroup)value;

            // This product has the original values.
            Product product = (Product)bindingGroup.Items[0];

            // Check the new values.
            string newModelName = (string)bindingGroup.GetValue(product, "ModelName");
            string newModelNumber = (string)bindingGroup.GetValue(product, "ModelNumber");

            if((newModelName == "") && (newModelNumber == ""))
            {
                return new ValidationResult(false,
                    "A product requires a ModelName or ModelNumber.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    /// <summary>
    /// BindingGroupValidation.xaml 的交互逻辑
    /// </summary>
    public partial class BindingGroupValidation : Window
    {
        public BindingGroupValidation()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }
    }
}
