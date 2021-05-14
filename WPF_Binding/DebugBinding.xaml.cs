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
    /// DebugBinding.xaml 的交互逻辑
    /// </summary>
    public partial class DebugBinding : Window
    {
        public DebugBinding()
        {
            InitializeComponent();
        }
    }
}




//调试数据绑定
//因为数据绑定是在运行时评估的，如果失败了也不会引发异常，糟糕的绑定有时候就很难找出来。这些问题会有几种不同的情况，但常见的一种情况是试图去绑定一个并不存在的属性，原因可能是把名字记错了，或者拼错了。请看这个例子：

//<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        Title="DataBindingDebuggingSample" Height="100" Width="200">
//    <Grid Margin = "10" Name="pnlMain">
//		<TextBlock Text = "{Binding NonExistingProperty, ElementName=pnlMain}" />

//    </ Grid >
//</ Window >
//“输出”窗口
//首先请看Visual Studio的输出窗口。这个窗口应该在Visual Studio的下边，也可以用快捷键[Ctrl + Alt + O] 来激活它。 窗口里会显示调试器的许多输出，不过在运行上面的例子时，其中应该能看到这么一行 例如：

//System.Windows.Data Error: 40 : BindingExpression path error: 'NonExistingProperty' property not found on 'object' ''Grid' (Name='pnlMain')'. BindingExpression:Path=NonExistingProperty; DataItem='Grid' (Name='pnlMain'); target element is 'TextBlock' (Name=''); target property is 'Text' (type 'String')

//这句话看上去很难理解，主要是因为很长，又没有断句，不过重点在这儿：

//'NonExistingProperty' property not found on 'object' ''Grid' (Name='pnlMain')'.

//这句话是说，在这个类型的Grid中的一个目标上使用了叫做“NonExistingProperty”（不存在的属性）的属性。这句话一语中的，能够帮助我们改正属性的名字，或者去绑定该正确的目标，就看具体是哪种错误。

//调整跟踪级别
//上面这个例子很容易解决，因为对WPF来说，我们的目的和错误原因都很明了。看下面这个例子：

//<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        Title="DataBindingDebuggingSample" Height="100" Width="200">
//    <Grid Margin = "10" >

//        < TextBlock Text="{Binding Title}" />
//	</Grid>
//</Window>
//我试图绑定到属性“Title”，但在哪个对象？正如关于数据上下文的文章中所述，WPF将在此处使用TextBlock上的DataContext属性，该属性可以在控件层次结构中继承，但在此示例中，我忘记分配数据上下文。这基本上意味着我正在尝试获取NULL对象的属性。 WPF将收集到这可能是一个完全有效的绑定，但该对象尚未初始化，因此它不会报错。如果运行此示例并查看输出窗口，则不会看到任何绑定错误。

//但是，这种情况不是期望的行为，有一种方法可以强制WPF告诉您遇到的所有绑定问题。 可以通过在PresentationTraceSources对象上设置TraceLevel来完成，该对象可以在System.Diagnostics命名空间中找到

//<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns:diag="clr-namespace:System.Diagnostics;assembly=WindowsBase"
//        Title="DataBindingDebuggingSample" Height="100" Width="200">
//    <Grid Margin = "10" >

//        < TextBlock Text="{Binding Title, diag:PresentationTraceSources.TraceLevel=High}" />
//	</Grid>
//</Window>
//请注意，我在顶部添加了对System.Diagnostics命名空间的引用，然后在绑定上使用了该属性。 WPF现在将在输出窗口中为您提供有关此特定绑定的加载信息：

//System.Windows.Data Warning: 55 : Created BindingExpression(hash= 2902278) for Binding(hash= 52760599)
//System.Windows.Data Warning: 57 :   Path: 'Title'
//System.Windows.Data Warning: 59 : BindingExpression(hash= 2902278): Default mode resolved to OneWay
//System.Windows.Data Warning: 60 : BindingExpression (hash= 2902278): Default update trigger resolved to PropertyChanged
//System.Windows.Data Warning: 61 : BindingExpression(hash= 2902278): Attach to System.Windows.Controls.TextBlock.Text (hash=18876224)
//System.Windows.Data Warning: 66 : BindingExpression(hash= 2902278): Resolving source
//System.Windows.Data Warning: 69 : BindingExpression(hash= 2902278): Found data context element: TextBlock(hash= 18876224) (OK)
//System.Windows.Data Warning: 70 : BindingExpression(hash= 2902278): DataContext is null
//System.Windows.Data Warning: 64 : BindingExpression(hash= 2902278): Resolve source deferred
//System.Windows.Data Warning: 66 : BindingExpression (hash= 2902278): Resolving source
//System.Windows.Data Warning: 69 : BindingExpression(hash= 2902278): Found data context element: TextBlock(hash= 18876224) (OK)
//System.Windows.Data Warning: 70 : BindingExpression(hash= 2902278): DataContext is null
//System.Windows.Data Warning: 66 : BindingExpression(hash= 2902278): Resolving source
//System.Windows.Data Warning: 69 : BindingExpression(hash= 2902278): Found data context element: TextBlock(hash= 18876224) (OK)
//System.Windows.Data Warning: 70 : BindingExpression(hash= 2902278): DataContext is null
//System.Windows.Data Warning: 66 : BindingExpression(hash= 2902278): Resolving source
//System.Windows.Data Warning: 69 : BindingExpression(hash= 2902278): Found data context element: TextBlock(hash= 18876224) (OK)
//System.Windows.Data Warning: 70 : BindingExpression(hash= 2902278): DataContext is null
//System.Windows.Data Warning: 66 : BindingExpression(hash= 2902278): Resolving source(last chance)
//System.Windows.Data Warning: 69 : BindingExpression(hash= 2902278): Found data context element: TextBlock(hash= 18876224) (OK)
//System.Windows.Data Warning: 77 : BindingExpression(hash= 2902278): Activate with root item<null>
//System.Windows.Data Warning: 105 : BindingExpression (hash= 2902278):   Item at level 0 is null - no accessor
//System.Windows.Data Warning: 79 : BindingExpression(hash= 2902278): TransferValue - got raw value {DependencyProperty.UnsetValue}
//System.Windows.Data Warning: 87 : BindingExpression(hash= 2902278): TransferValue - using fallback/default value ''
//System.Windows.Data Warning: 88 : BindingExpression(hash= 2902278): TransferValue - using final value ''
//通过阅读列表，您实际上可以看到WPF经历的整个过程，尝试为TextBlock控件找到正确的值。 你会发现多次它无法找到合适的DataContext，最后，它使用默认的{DependencyProperty.UnsetValue}转换为空字符串。

//使用真正的调试器
//上述技巧可以很好地诊断错误的绑定，但在某些情况下，使用真正的调试器更简单合适。 绑定本身并不支持这一点，因为它们是在WPF内部处理的，但可以使用转换器，如前一篇文章中所示，您实际上可以跳转到此过程并逐步完成它。 你需要一个不做任何实际转换的转换器，你只需要一个进入绑定过程的方法，一个虚拟转换器将带你到那里：

//<Window x:Class="WpfTutorialSamples.DataBinding.DataBindingDebuggingSample"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns:self="clr-namespace:WpfTutorialSamples.DataBinding"
//        Title="DataBindingDebuggingSample" Name="wnd" Height="100" Width="200">
//	<Window.Resources>
//		<self:DebugDummyConverter x:Key="DebugDummyConverter" />
//	</Window.Resources>
//    <Grid Margin = "10" >

//        < TextBlock Text="{Binding Title, ElementName=wnd, Converter={StaticResource DebugDummyConverter}}" />
//	</Grid>
//</Window>
//using System;
//using System.Windows;
//using System.Windows.Data;
//using System.Diagnostics;

//namespace WpfTutorialSamples.DataBinding
//{
//    public partial class DataBindingDebuggingSample : Window
//    {
//        public DataBindingDebuggingSample()
//        {
//            InitializeComponent();
//        }
//    }

//    public class DebugDummyConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            Debugger.Break();
//            return value;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            Debugger.Break();
//            return value;
//        }
//    }
//}
//在后台代码文件中，我们定义了一个DebugDummyConverter。 有Convert()和ConvertBack()方法，我们调用Debugger.Break()，它与在Visual Studio中设置断点具有相同的效果，返回值不变。

//在标记中，我们在窗口资源中添加对转换器的引用，然后在绑定中使用它。 在实际应用程序中，您应该将转换器定义在它自己的文件中，然后在App.xaml中添加对它的引用，以便您可以在整个应用程序中使用它，而无需在每个窗口中创建对它的新引用。但对于这个例子，上面的做法很好。

//如果运行该示例，一旦WPF尝试获取窗口标题的值，您将看到调试器中断。 现在，您可以使用Visual Studio的标准调试功能检查赋予Convert()方法的值，甚至在继续之前更改它们。

//如果调试器永远不会中断，则意味着没有使用转换器。 这通常表示您的绑定表达式是无效的，可以使用本文开头所述的方法对其进行诊断和修复。 虚拟转换器技巧仅用于测试有效的绑定表达式。
