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

namespace WPF_Image
{
    /// <summary>
    /// ImageSourceInCS.xaml 的交互逻辑
    /// </summary>
    public partial class ImageSourceInCS : Window
    {
        public ImageSourceInCS()
        {
            InitializeComponent();

            //注意哪些是绝对路径哪些是相对路径   (UriKind.Absolute UriKind.Relative)
            image.Source = new BitmapImage(new Uri("pack://application:,,,/WPF_Image;component/Image/google.png", UriKind.Absolute));
            image2.Source = new BitmapImage(new Uri("pack://application:,,,/Image/google.png", UriKind.Absolute));
            image3.Source = new BitmapImage(new Uri("/WPF_Image;component/Image/google.png", UriKind.Relative));
            image4.Source = new BitmapImage(new Uri("/Image/google.png", UriKind.Relative));
        }
    }
}
