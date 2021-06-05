using StoreDatabase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_DataBinding
{
    public class AdvancedPriceToBackgroundConverter : IValueConverter
    {
        private Brush highlightBrush;
        public Brush HighlightBrush
        {
            get { return highlightBrush; }
            set { highlightBrush = value; }
        }

        private decimal minPrice;
        public decimal MinPrice
        {
            get { return minPrice; }
            set { minPrice = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Product product = (Product)value;

            if(product.UnitCost >= MinPrice)
            {
                return HighlightBrush;
            }
            else
            {
                return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
