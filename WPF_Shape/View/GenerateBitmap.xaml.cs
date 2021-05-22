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

namespace WPF_Shape.View
{
    /// <summary>
    /// GenerateBitmap.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateBitmap : Window
    {
        public GenerateBitmap()
        {
            InitializeComponent();
        }


        private void cmdGenerate2_Click(object sender, RoutedEventArgs e)
        {
            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap((int)img.Width,
                (int)img.Height, 96, 96, PixelFormats.Bgra32, null);

            Random rand = new Random();
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                for (int y = 0; y < wb.PixelHeight; y++)
                {
                    int alpha = 0;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if ((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                    }
                    else
                    {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                    }

                    // Set the pixel value.                    
                    byte[] colorData = { (byte)blue, (byte)green, (byte)red, (byte)alpha }; // B G R

                    //当调用WritePixels()方法时，提供Int32Rect对象以指定位图中希望更新的矩形区域
                    //Int32Rect封装了4部分信息：更新区域左上角的X和Y坐标，以及更新区域的宽度和高度
                    Int32Rect rect = new Int32Rect(x, y, 1, 1);

                    //跨距
                    //跨距时每行像素数据需要的字节数量。可通过将每行中像素的数量乘上所使用格式的每像素位数(通常为32，如本例使用的Bgra32格式)，然后
                    //将所得结果除以8，进而将其从位数转换成字节数
                    int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;
                    wb.WritePixels(rect, colorData, stride, 0);

                    //wb.WritePixels(.[y * wb.PixelWidth + x] = pixelColorValue;
                }
            }

            // Show the bitmap in an Image element.
            //完成每个像素的生成过程后，需要显示最终位图。通常将使用Image元素完成该工作
            img.Source = wb;

            //即使是在写入和显示位图后，也仍可自由地读取和修改WriteableBitmap对象中的像素，从而可以构建更特殊的用于位图编辑以及位图命中测试的例程
        }


        //更高效的像素写入

        //尽管上面的代码可以工作，但并非最佳方法。如果需要一次性写入大量像素数据——甚至是整幅图像——最好使用更大的快，因为调用
        //WritePixels()方法需要一定的开销，并且调用该方法越频繁，应用程序的运行速度就越慢

        //为一次更新多个像素，需要理解像素被打包进字节数组的方式。无论使用哪种方式，更新缓冲区都将包括一维字节数组。这个数组提供了
        //用于图像矩形区域中像素的数值，从左向右延申填充每行，然后自上而下延申
        private void cmdGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Create the bitmap, with the dimensions of the image placeholder.
            WriteableBitmap wb = new WriteableBitmap((int)img.Width, (int)img.Height, 96, 96, PixelFormats.Bgra32, null);

            // Define the update square (which is as big as the entire image).
            Int32Rect rect = new Int32Rect(0, 0, (int)img.Width, (int)img.Height);

            byte[] pixels = new byte[(int)img.Width * (int)img.Height * wb.Format.BitsPerPixel / 8];
            Random rand = new Random();
            for(int y = 0; y < wb.PixelHeight; ++y)
            {
                for(int x = 0; x < wb.PixelWidth; ++x)
                {
                    int alpha = 0;
                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    // Determine the pixel's color.
                    if((x % 5 == 0) || (y % 7 == 0))
                    {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                    }
                    else
                    {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                    }

                    //为找到某个特定像素，需要使用以下公式，下移数行，然后移到该行中恰当的位置：
                    //例如，为设置一幅Bgra32格式(每像素具有4个字节)的位图中的像素(40, 100)，需要使用下面的代码:
                    //int pixelOffset = (40 + 100 * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    //pixels[pixelOffset] = blue;
                    //pixels[pixelOffset + 1] = green;
                    //pixels[pixelOffset + 2] = red;
                    //pixels[pixelOffset + 3] = alpha;
                    int pixelOffset = (x + y * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = (byte)blue;
                    pixels[pixelOffset + 1] = (byte)green;
                    pixels[pixelOffset + 2] = (byte)red;
                    pixels[pixelOffset + 3] = (byte)alpha;
                }

                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                wb.WritePixels(rect, pixels, stride, 0);
            }

            //Show the bitmap in an Image element.
            img.Source = wb;
        }
        //在实际应用程序中，可选择折中方法。如果需要更新位图中一块较大的区域，不会一次写入一个像素，因为这种方法的运行速度太慢。
        //但也不会在内存中同时保存全部图像数据，因为图像数据可能会很大(毕竟，一幅每像素需要4个字节的1000*1000像素的图像需要将近
        //4MB的内存，这一要求不是很过分，但是也比较高)。相反，应当写入一大块图像数据而不是单个像素，当一次生成一整幅位图时尤其如此

        //如果需要频繁更新WriteableBitmap对象中的图像数据，并希望在另一个线程中执行这些更新，可以使用WriteableBitmap后台缓冲区
        //以进一步优化代码。基本过程是：使用Lock()方法预订后台缓冲区，获得指向后台缓冲区的指针，更新后台缓冲区，调用AddDirtyRect()
        //方法指示已经改变的区域，然后通过调用UnLock()方法释放后台缓冲区。
    }
}
