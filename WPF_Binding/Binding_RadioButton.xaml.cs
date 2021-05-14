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

namespace WPF_Binding
{
    public class EnumToBoolConverter : IValueConverter
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

    public enum Sex
    {
        Male,
        Female
    }

    /// <summary>
    /// Binding_RadioButton.xaml 的交互逻辑
    /// </summary>
    public partial class Binding_RadioButton : Window
    {
        public Binding_RadioButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控件RadioButton中值的修改会影响_personSex      如果反过来也有效还需要实现INotifyPropertyChanged接口，这里省略
        /// </summary>
        private Sex _personSex;
        public Sex PersonSex
        {
            get
            {
                return _personSex;
            }
            set
            {
                if(value != _personSex)
                {
                    _personSex = value;

                    //
                }
            }
        }
    }
}
