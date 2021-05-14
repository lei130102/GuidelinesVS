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

namespace WPF_Binding
{
    /// <summary>
    /// StringFormat_ConverterCulture.xaml 的交互逻辑
    /// </summary>
    public partial class StringFormat_ConverterCulture : Window
    {
        public StringFormat_ConverterCulture()
        {
            InitializeComponent();
        }
    }
}



//就像我们在上一章中看到的,通常用来修改绑定输出的方式是通过使用转换器.转换器的强大之处在于它允许你将任意数据类型转换为另一个完全不同的数据类型.然而,相对于大多数应用场景,你只是想改变某些值的显示方式而没有必要将其转换成另一个不同的类型,StringFormat属性则可以很好的做到这一点.

//使用一个绑定的 StringFormat 属性时,你会丢失一些使用转换器时的灵活性,但相应地,它会更简单易用且不会在新文件中创建一个新类.

//StringFormat 属性的的功能就如同它的名字所表达的: 它通过简单的调用 String.Format 方法来格式化输出字符串.我们先来看一个简短的示例:



//前几个文本块通过绑定到它们的父窗口,得到窗口的高度和宽度来作为它们的值.这些值已经用 StringFormat 属性格式化.对于宽度,我们指定了一个自定义的格式化字符串;对于高度,我们对它使用货币格式,只是为了有趣.值是保存为double 类型的, 所以如果我们调用了double.ToString(),我们可以使用所有相似的格式指示符.您可以在这里找到一个格式指示符的列表: http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx

//另请注意我如何在StringFormat中包含自定义文本 - 这允许您根据需要使用文本预先/后固定绑定值。当引用格式字符串中的实际值时，我们用一组花括号括起来，它包括两个值：对我们要格式化的值的引用（值0，这是第一个可能的值）和格式字符串，用冒号分隔。

//对于最后两个值，我们只是绑定到当前日期（DateTime.Now）并将其作为日期，以特定格式输出，然后再作为时间（小时和分钟），再次使用我们自己的，预先定义的格式。您可以在此处阅读有关DateTime格式的更多信息：http://msdn.microsoft.com/en-us/library/az4se3k1.aspx



