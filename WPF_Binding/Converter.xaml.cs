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
    class YesNoStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "yes":
                    return true;
                case "no":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                {
                    return "yes";
                }
                else
                {
                    return "no";
                }
            }
            return Binding.DoNothing;//用作返回值以指示绑定引擎不执行任何操作。
        }

        //如果不提供ConvertBack，那么可以这样实现：
        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}
    }

    /// <summary>
    /// Converter.xaml 的交互逻辑
    /// </summary>
    public partial class Converter : Window
    {
        public Converter()
        {
            InitializeComponent();
        }
    }
}


//使用IValueConverter进行值转换
//到目前为止我们已经使用了一些简单的，可以同步属性的数据绑定。然而，你将会遇见想要使用一种类型，但需要以不同方式呈现的场景。

//何时使用值转换器
//值转换器经常与数据绑定一起使用。以下是一些基本示例：

//您有一个数值，但您希望以一种方式显示零值，而以另一种方式显示正数
//您想要根据值检查CheckBox，但值是一个字符串，如“是”或“否”而不是布尔值
//您有一个以字节为单位的文件大小，但您希望根据它的大小显示为字节，千字节，兆字节或千兆字节。
//这些是一些简单的案例，但还有更多。例如，您可能希望根据布尔值检查复选框，但是您希望它反转，以便检查CheckBox是否为false，如果值为true则不检查。您甚至可以使用转换器根据值生成一个ImageSource的图像，如绿色标志为true或红色标志为false - 可能性几乎无穷无尽！

//对于这种情况，您可以使用值转换器。这些实现IValueConverter接口的小类将像中间人一样工作，并在源和目标之间转换值。因此，在任何需要在值到达目的地之前转换或再次返回其源的情况下，您可能需要转换器。

//实现一个简单的值转换器
//如上所述，WPF值转换器需要实现IValueConverter接口，或者IMultiValueConverter接口（稍后将详细介绍）。这两个接口只需要您实现两个方法：Convert()和ConvertBack()。顾名思义，这些方法将用于将值转换为目标格式，然后再返回。


//后台代码
//所以，让我们从后面开始，然后通过这个例子。我们在代码隐藏文件中实现了一个名为YesNoToBooleanConverter的转换器。正如所宣传的那样，它只实现了两个必需的方法，称为Convert()和ConvertBack()。 Convert()方法假定它接收一个字符串作为输入（value参数），然后将其转换为布尔值true或false值，后备值为false。为了好玩，我添加了从法语单词进行转换的可能性。

//ConvertBack()方法显然正好相反：它假设一个布尔类型的输入值，然后返回英文单词“yes”或“no”作为回报，后退值为“no”。

//您可能想知道这两种方法采用的其他参数, 但在此示例中不需要它们。我们将在接下来的一章中使用它们, 在那里我们将对它们进行解释。

//XAML
//在程序的XAML部分，我们首先将转换器的实例声明为窗口的资源。然后我们有一个TextBox，一些TextBlocks和一个CheckBox控件，这就是有趣的事情发生的地方：我们将TextBox的值绑定到TextBlock和CheckBox控件，并使用Converter属性和我们自己的转换器引用，我们根据需要，在字符串和布尔值之间来回处理值。

//如果您尝试运行此示例，您将能够在两个位置更改值：在TextBox中写入“yes”（或任何其他值，如果您想要false）或通过选中CheckBox。无论您做什么，更改都将反映在其他控件以及TextBlock中。


