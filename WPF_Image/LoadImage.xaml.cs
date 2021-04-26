using Microsoft.Win32;
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

//动态加载图片（后置代码）
//直接在XAML中指定图片源可以解决很多情况，但有时您需要动态加载图片，例如 基于用户选择。 这可以从后置代码做到。
//以下是根据从OpenFileDialog中选择的方式加载用户计算机上的图片的方法：

namespace WPF_Image
{
    /// <summary>
    /// LoadImage.xaml 的交互逻辑
    /// </summary>
    public partial class LoadImage : Window
    {
        public LoadImage()
        {
            InitializeComponent();
        }

        private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
            }
        }

        private void btnLoadFromResource_Click(object sender, RoutedEventArgs e)
        {
            Uri resourceUri = new Uri("/Image/google.png", UriKind.Relative);
            imgDynamic.Source = new BitmapImage(resourceUri);
        }
    }
}

//Stretch属性
//在Source属性之后，显而易见这很重要，我认为Image控件的第二个最有趣的属性可能是Stretch属性。
//它控制当加载的图片尺寸与Image控件的尺寸不完全匹配时怎么处理。 这将经常发生，因为窗口的大小可以由用户控制，除非您的布局非常静态，
//这意味着Image控件的大小也会改变。
