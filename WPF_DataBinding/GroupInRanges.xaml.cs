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
    public class PriceRangeProductGrouper : IValueConverter
    {
        public int GroupInterval
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            if(price < GroupInterval)
            {
                return String.Format("Less than {0:C}", GroupInterval);
            }
            else
            {
                int interval = (int)price / GroupInterval;
                int lowerLimit = interval * GroupInterval;
                int upperLimit = (interval + 1) * GroupInterval;
                return String.Format("{0:C} to {1:C}", lowerLimit, upperLimit);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This converter is for grouping only.");
        }
    }

    /// <summary>
    /// GroupInRanges.xaml 的交互逻辑
    /// </summary>
    public partial class GroupInRanges : Window
    {
        public GroupInRanges()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            CollectionViewSource viewSource = (CollectionViewSource)this.FindResource("GroupByRangeView");
            viewSource.Source = products;

            //lstProducts.ItemsSource = products;

            //ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("UnitCost", ListSortDirection.Ascending));
            //PriceRangeProductGrouper grouper = new PriceRangeProductGrouper();
            //grouper.GroupInterval = 50;
            //view.GroupDescriptions.Add(new PropertyGroupDescription("UnitCost", grouper));
        }
    }
}
