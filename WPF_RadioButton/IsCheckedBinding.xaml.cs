using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPF_RadioButton
{
    public enum Sex
    {
        Male,
        Female
    }

    public class SexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? false : value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null) && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    /// <summary>
    /// IsCheckedBinding.xaml 的交互逻辑
    /// </summary>
    public partial class IsCheckedBinding : Window, INotifyPropertyChanged
    {
        public IsCheckedBinding()
        {
            InitializeComponent();
        }

        private Sex _sex;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public Sex Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                if (_sex != value)
                {
                    _sex = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Sex"));
                }
            }
        }
    }
}
