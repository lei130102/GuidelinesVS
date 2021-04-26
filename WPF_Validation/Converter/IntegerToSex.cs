using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Validation.Converter
{
    public class IntegerToSex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return "男";
            }

            Int32 valueB = (Int32)value;
            switch(valueB)
            {
                case 1:
                    return "男";
                case 0:
                    return "女";
            }
            return "男";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
