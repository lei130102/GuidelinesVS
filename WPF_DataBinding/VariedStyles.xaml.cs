using StoreDatabase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
    public class SingleCriteriaHighlightStyleSelector : StyleSelector
    {
        public Style DefaultStyle
        {
            get;
            set;
        }

        public Style HighlightStyle
        {
            get;
            set;
        }

        public string PropertyToEvaluate
        {
            get;
            set;
        }

        public string PropertyValueToHighlight
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Product product = (Product)item;

            // Use reflection to get the property to check.
            Type type = product.GetType();
            PropertyInfo property = type.GetProperty(PropertyToEvaluate);

            // Decide if this product should be highlighted
            // based on the property value.
            if(property.GetValue(product, null).ToString() == PropertyValueToHighlight)
            {
                return HighlightStyle;
            }
            else
            {
                return DefaultStyle;
            }
        }
    }

    /// <summary>
    /// VariedStyles.xaml 的交互逻辑
    /// </summary>
    public partial class VariedStyles : Window
    {
        public VariedStyles()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdApplyChange_Click(object sender, RoutedEventArgs e)
        {
            ((ObservableCollection<Product>)products)[1].CategoryName = "Travel";
            StyleSelector selector = lstProducts.ItemContainerStyleSelector;
            lstProducts.ItemContainerStyleSelector = null;
            lstProducts.ItemContainerStyleSelector = selector;
        }
    }
}
