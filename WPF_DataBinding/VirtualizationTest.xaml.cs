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


//提高大列表的性能

//如果处理大量数据——例如，数万条记录而不止几百条——您知道良好的数据绑定系统不仅仅需要绑定功能，还需要能够处理超大量的数据而不会严重降低显示速度或
//消耗大量的内存。幸运的是，WPF优化了其列表控件以为您提供帮助

//接下来将讨论针对大列表的几个性能增强特性，所有WPF列表控件(所有继承自ItemsControl的控件)都支持这些增强特性，包括低级的ListBox和ComboBox，更
//专业的ListView、TreeView和DataGrid












//提高大列表的性能——虚拟化

//WPF列表控件提供的最重要功能是UI虚拟化(UI virtualization)，UI虚拟化是列表仅为当前显示项创建容器对象的一种技术

//例如，如果有一个具有50000条记录的ListBox控件，但可见区域只能包含30条记录，ListBox控件将只创建30个ListBoxItem对象(为了确保良好的滚动性能，
//会再增加几个ListBoxItem对象)。如果ListBox控件不支持UI虚拟化，就需要生成全部50000个ListBoxItem对象，这显然需要占用更多的内存。更有意义的是，
//分配这些对象需要的时间能够明显感觉到，当代码设置ListBox.ItemsSource属性时这会短暂地锁定应用程序

//UI虚拟化支持实际上没有被构建进ListBox或ItemsControl类。相反，而是被硬编码到VirtualizingStackPanel容器，除增加了虚拟化支持，该面板和StackPanel
//面板地功能类似。ListBox、ListView以及DataGrid都自动使用VirtualizingStackPanel面板来布局他们地子元素。所以，为了获得虚拟化支持，不需要采取任何
//额外地步骤。然而，ComboBox类使用标准地没有虚拟化支持地StackPanel面板。如果需要虚拟化支持，就必须明确地通过提供新地ItemsPanelTemplate来增加
//虚拟化支持，如下所示：
//<ComboBox>
//    <ComboBox.ItemsPanel>
//        <ItemsPanelTemplate>
//            <VirtualizingStackPanel></VirtualizingStackPanel>
//        </ItemsPanelTemplate>
//    </ComboBox.ItemsPanel>
//</ComboBox>
//TreeView是另一个支持虚拟化的控件，但再默认情况下，他关闭了该支持。问题是在早期的WPF发布版本中，VirtualizingStackPanel面板不支持层次化数据。
//现在虽然支持，但TreeView禁用了该特性以确保向后兼容性。幸运的是，只通过设置一个属性即可启用该特性，在包含大量数据的树控件中总是推荐启用该特性：
//<TreeView VirtualizingStackPanel.IsVirtualizing="True"...>

//注意：从技术角度看，VirtualizingStackPanel继承自抽象类VirtualizingPanel。如果想要使用不同类型的虚拟化面板，比如支持虚拟化的Grid面板，就需要
//从第三方组件供应商那里购买


//有许多因素可能会破坏UI虚拟化支持，而且有时是意想不到的：
//1.在ScrollViewer中放置列表控件
//ScrollViewer为其子内容提供了一个窗口。问题是为子内容提供了无限的"虚拟"空间。在这个虚拟空间中，ListBox以完整尺寸渲染自身，显示所有子项。副作用是，
//每项在内存中都有各自的ListBoxItem对象。只要将ListBox控件放入不会试图限制其尺寸的容器中，就会发生这一问题；例如，如果将ListBox控件放到StackPanel
//面板而不是Grid面板中，也会发生类似问题
//2.改变列表控件的模板并且没有使用ItemsPresenter。
//ItemsPresenter使用ItemsPanelTemplate，该模板指定了VirtualizingStackPanel面板。如果破坏了这种关系或自定改变了ItemsPanelTemplate，从而使其不使用
//VirtualizingStackPanel面板，将会丢失虚拟化特性
//3.不使用数据绑定
//这应当是显而易见的，但如果通过编程填充列表——例如，通过动态创建需要的ListBoxItem对象——那么不会发生虚拟化。当然，可考虑使用自己的优化策略，例如创建
//所需的对象并只在需要时创建，之后的有示例演示了如何为TreeView控件使用这种技术(当使用即使节点创建以填充目录树时)

//如果有一个大列表，需要避免这些问题以确保得到良好的性能

//即使当使用UI虚拟化时，仍然必须为实例化内存中的数据对象付出代价。例如，在具有50000项的ListBox控件示例中，仍有50000个数据对象，每个对象具有与产品、客户、
//订单记录或其他内容相关的不同数据。如果希望优化应用程序的这一部分，可考虑使用数据虚拟化(data virtualization)——每次只获取一批记录的一种技术。数据虚拟化
//是更复杂的技术，因为他假定检索数据的代价比保存数据的代价更低。根据数据的大小和检索数据所需的时间，这不一定是正确的。例如，如果当用户在列表中滚动时，应用
//程序不断地连接到网络数据库以获取更多的产品信息，最终结果会减低滚动性能，并会增加数据库服务器的负担

//当前，WPF没有提供任何支持数据虚拟化的控件或类。然而，这不会阻止企业级开发人员创建这一缺失的功能：假装具有所有项的“伪”集合，但直到控件需要数据时才从后台
//数据源中查询数据







//提高大列表的性能——项容器再循环

//通常当滚动支持虚拟化列表时，控件不断地创建新的项容器对象以保存新的可见项。
//例如，当在具有50000个项的ListBox控件中滚动时，ListBox控件将生成新的ListBoxItem对象。但如果启动了项容器再循环，ListBox控件将只保持少量ListBoxItem
//对象存活，并当滚动时通过新数据加载这些ListBoxItem对象，从而重复使用他们
//<ListBox VirtualizingStackPanel.VirtualizationMode="Recycling" ...>
//项容器再循环提高了滚动性能，降低了内存消耗量，因为垃圾收集器不需要查找旧的项对象并释放他们。通常，为了确保向后兼容，对于除DataGrid之外的所有控件，
//该特性默认是禁用的。如果有一个大列表，应当总是启用该特性








//提高大列表的性能——缓存长度

//如前所述，VirtualizingStackPanel创建了几个超过其显示范围的附加项。这样，再开始滚动时，可以立即显示这些项
//在以前的WPF版本中，将多个附加项硬编码到VirtualizingStackPanel中。但在WPF4.5中，您可使用CacheLength和CacheLengthUnit这两个VirtualizingStackPanel
//属性进一步调整精确数量。CacheLengthUnit允许选择如何指定附加项的数量：项数、页数(其中，单页包含适应于控件可视"窗口"的所有项)或像素数(如果项显示不同大小
//的图片，这将是合理选择)
//默认的CacheLength和CacheLengthUnit属性在当前可见项之前和之后存储项的附加页，如下所示：
//<ListBox VirtualizingStackPanel.CacheLength="1" VirtualizingStackPanel.CacheLengthUnit="Page" .../>
//下面的代码正好在当前可见项之前存储100项，在当前可见项之后存储100项：
//<ListBox VirtualizingStackPanel.CacheLength="100" VirtualizingStackPanel.CacheLengthUnit="Item" .../>
//下面的代码在当前可见项之前存储100项，在当前可见项之后存储500项(原因可能是您预估用户将耗费大部分时间向下滚动，而不是向上滚动)：
//<ListBox VirtualizingStackPanel.CacheLength="100,500" VirtualizingStackPanel.CacheLengthUnit="Item" .../>
//有必要指出，附加项的缓存用背景来填充。这意味着，VirtualizingStackPanel将立即显示创建的可见项集。此后，VirtualizingStackPanel将开始在优先级较低的
//后台线程上填充缓存，因此不能锁定应用程序








//提高大列表的性能——延迟滚动

//为进一步提高滚动性能，可开启延迟滚动(deferred scrolling)特性。使用延迟滚动特性，当用户在滚动条上拖动滚动滑块时不会更新列表显示。只有当用户释放了
//滚动滑块时才刷新。比较起来，当使用常规滚动时，在拖动的同时会刷新列表，从而使列表显示正在改变的位置。

//与为列表控件使用项容器再循环一样，需要明确地启用延迟滚动特性：
//<ListBox ScrollViewer.IsDeferredScrollingEnabled="True" .../>

//显然，需要在响应性和易用性之间取得平衡。如果有一个复杂的模板和大量数据，对于提高速度可能更愿意使用延迟滚动特性。但与此相反，当滚动时用户可能更愿意
//能够查看目前滚动到了什么位置
//VirtualizingStackPanel通常使用基于项的滚动(item-based scrolling)。这意味着当向下滚动少许时，下一项将显示出来。无法滚动查看项的一部分。无论是单击
//滚动条，单击滚动箭头，还是调用诸如ListBox.ScrollIntoView()的方法，在面板上至少会滚动一个完整项
//然而，可通过将VirtualizingStackPanel.ScrollUnit属性设置为Pixel来覆盖该行为，并使用基于像素的滚动：
//<ListBox VirtualizingStackPanel.ScrollUnit="Pixel" .../>
//此时，便可以向下滚动来查看项的一部分了。
//应该根据在列表中显示的内容类型以及个人爱好，在“基于项的滚动”与“基于像素的滚动”之间加以选择。一般而言，基于像素的滚动更流畅，因为他允许使用较小的
//滚动间隔；而基于项的滚动更清晰，因为始终可看到项的全部内容








namespace WPF_DataBinding
{
    /// <summary>
    /// VirtualizationTest.xaml 的交互逻辑
    /// </summary>
    public partial class VirtualizationTest : Window
    {
        public VirtualizationTest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 10000; ++i)
            {
                lstFast.Items.Add(i.ToString());
                lstSlow.Items.Add(i.ToString());
            }
        }
    }
}
