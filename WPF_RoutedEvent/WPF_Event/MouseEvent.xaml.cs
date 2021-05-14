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

//鼠标事件

//MouseEnter  直接路由事件  当鼠标指针移到元素上时引发该事件
//MouseLeave  直接路由事件  当鼠标指针离开元素时引发该事件
//(如果有一个包含按钮的StackPanel面板，并将鼠标指针移到按钮上，那么首先会为这个StackPanel面板引发MouseEnter事件(当鼠标指针进入StackPanel面板
//的边界时)，然后为按钮引发MouseEnter事件(当鼠标指针移到按钮上时)。将鼠标指针移开时，首先为按钮，然后为StackPanel面板引发MouseLeave事件)
//(UIElement类还包含两个有用的属性，这两个属性能帮助进行鼠标命中测试。可使用IsMouseOver属性确定当前鼠标是否位于某个元素及其子元素上面，还可以使用
//IsMouseDirectlyOver属性检查鼠标是否位于某个元素上面，但未位于其子元素上面。通常不会在代码中读取和使用这些值，反而会使用他们构建样式触发器，
//从而当鼠标移到元素上时，自动修改元素)

//PreviewMouseMove  隧道路由事件   当鼠标移动
//MouseMove         冒泡路由事件   当鼠标移动
//(都提供了MouseEventArgs对象，该对象包含当事件发生时识别鼠标状态的属性以及GetPosition()方法，该方法返回相对于所选元素的鼠标坐标)

//鼠标单击事件的引发方式和按键事件的引发方式有类似之处，区别是对于鼠标左键和鼠标右键引发不同的事件
//PreviewMouseLeftButtonDown      隧道路由事件     当按下鼠标左键时发生
//PreviewMouseRightButtonDown     隧道路由事件     当按下鼠标右键时发生
//MouseLeftButtonDown             冒泡路由事件     当按下鼠标左键时发生
//MouseRightButtonDown            冒泡路由事件     当按下鼠标右键时发生
//PreviewMouseLeftButtonUp        隧道路由事件     当释放鼠标左键时发生
//PreviewMouseRightButtonUp       隧道路由事件     当释放鼠标右键时发生
//MouseLeftButtonUp               冒泡路由事件     当释放鼠标左键时发生
//MouseRightButtonUp              冒泡路由事件     当释放鼠标右键时发生

//PreviewMouseWheel               隧道路由事件     当鼠标滚轮动作时发生
//MouseWheel                      冒泡路由事件     当鼠标滚轮动作时发生

//(都提供了MouseButtonEventArgs对象，他派生自MouseEventArgs，其中有：
//MouseButton        通知是哪个鼠标键引发的事件
//ButtonState        通知当事件发生时鼠标键是处于按下状态还是释放状态
//ClickCount         通知鼠标键被单击了多少次，从而区分是单击(ClickCount的值为1)还是双击(ClickCount的值为2)
//)

//(通常，当单击鼠标时，Windows应用程序对鼠标键的 释放(Up) 事件进行响应)

//(某些元素添加了更高级的鼠标事件：
//Control类添加了PreviewMouseDoubleClick和MouseDoubleClick事件，这两个事件代替了MouseLeftButtonUp事件
//Button类通过鼠标或键盘可触发Click事件
//)

//(与键盘按键事件一样，当发生鼠标事件时，这些事件提供了有关鼠标位置和哪个鼠标键被按下的信息。为获得当前鼠标位置和按键状态，可使用Mouse类
//的静态成员，他们和MouseButtonEventArgs类的成员类似)

//某些情况下，可能希望通知鼠标键释放事件，即使鼠标键释放事件是在鼠标已经离开了原来的元素之后发生的。为此，需要调用Mouse.Capture()方法并
//传递恰当的元素以捕获鼠标。此后，就会接收到鼠标键按下事件和释放事件，直到再次调用Mouse.Capture()方法并传递空引用为止
//(当鼠标被一个元素捕获后，其他元素就不会接收到鼠标事件。这意味着用户不能单击窗口中其他位置的按钮，不能单击文本框的内部。鼠标捕获有时用于
//可以被拖放并可以改变尺寸的元素，鼠标捕获通常用于短时间的操作)
//(当调用Mouse.Capture()方法时，可传递可选的CaptureMode值作为第二个参数，通常，当调用Mouse.Capture()方法时，使用CaptureMode.Element值，
//这表示元素总是接收鼠标事件。然而，如果使用CaptureMode.SubTree，鼠标事件就可以经过已单击的元素(假定这个元素是执行捕获的元素的子元素)，如果
//在子元素中已经使用了事件冒泡或隧道特性来监视鼠标事件，这是非常合理的)

//有些情况下，可能由于其他原因(不是您的错)丢失鼠标捕获。例如，如果需要显示系统对话框，Windows可能会释放鼠标捕获。如果当鼠标键释放事件发行后
//没有释放鼠标，并且用户单击了另一个应用程序中的窗口，也可能丢失鼠标捕获。无论哪种情况，都可以通过处理元素的LostMouseCapture事件来响应鼠标捕获的丢失

//鼠标拖放
//拖放操作通过以下三个步骤完成：
//1.用户单击元素(或选择元素中的一块特定区域)，并保持鼠标键为按下状态。这时，某些信息被搁置起来，并且拖放操作开始
//2.用户将鼠标移到其他元素上。如果该元素可接受正在拖动的内容的类型（例如一幅位图或一块文本），鼠标指针会变成拖放图标，否则鼠标指针会变成内部有一条线的图形
//3.当用户释放鼠标键时，元素接收信息并决定如果处理接收到的信息。在没有释放鼠标键时，可按下Esc键取消该操作
//(用于拖放操作的方法和事件都集中在System.Windows.DragDrop类中。通过使用该类，任何元素都可以参与拖放操作)

//TextBox控件提供了支持拖放的内置逻辑。如果选中文本框中的一些文本，就可以将这些文本拖动到另一个文本框中。当释放鼠标键时，这些文本将移动位置。




//拖放操作有两个方面：源和目标
//源：
//为了创建拖放源，需要在某个位置调用DragDrop.DoDragDrop()方法来初始化拖放操作。此时确定拖动操作的源，搁置希望移动的内容，
//并指明允许什么样的拖放效果(复制、移动等)，通常，在响应MouseDown或者PreviewMouseDown事件时调用DoDragDrop()方法
//目标：
//接收数据的元素需要将他的AllowDrop属性设置为true，此外，他还需要通过处理Drop事件来处理数据。将AllowDrop属性设置为True时，
//就将元素配置为允许任何类型的信息。如果希望有选择地接收内容，可处理DragEnter事件。这时，可以检查正在拖动的内容的数据类型，
//然后确定所允许的操作类型。下面的示例只允许文本内容——如果拖动的内容不能被转换成文本，就不允许执行拖动操作，鼠标指针会变成
//内部具有一条线的圆形光标，表示禁止操作

//如果希望将内容拖放到其他应用程序中，应当使用基本数据类型（如字符串、整型等），或者使用实现了ISerializable和IDataObject接口
//的对象(这两个接口允许.NET将对象转换成字节流，并在另一个应用程序域中重新构造对象)。一个有趣的技巧是将WPF对象转换成XAML，并
//在其他地方重新构成该WPF对象。所需要的所有对象就是XamlWriter和XamlReader对象
//(如果希望在两个应用程序之前传递数据，那么务必检查System.Windows.Clipboard类，该类提供了静态方法，用于在Windows剪贴板中放置数据，
//并以各种不同的格式检索剪贴板中的数据)

namespace WPF_RoutedEvent.WPF_Event
{
    /// <summary>
    /// MouseEvent.xaml 的交互逻辑
    /// </summary>
    public partial class MouseEvent : Window
    {
        public MouseEvent()
        {
            InitializeComponent();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this);
            lblInfo.Text = String.Format("You are at ({0},{1}) in window coordinates", pt.X, pt.Y);

            //这里坐标有可能不是整数，这是可能是因为当前系统DPI被设置为120dpi(而不是标准的96dpi)，WPF自动缩放其单位，使用更多的物理像素
            //进行补偿。因为屏幕像素的大小不再和WPF单位系统的尺寸匹配，所以鼠标的物理位置可能被转换成小数形式的WPF单位
        }

        private void cmdCapture_Click(object sender, RoutedEventArgs e)
        {
            this.AddHandler(Mouse.LostMouseCaptureEvent, new RoutedEventHandler(this.LostCapture));

            Mouse.Capture(rect);

            cmdCapture.Content = "[ Mouse is now captured ...]";
        }

        private void LostCapture(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lost capture");

            cmdCapture.Content = "Capture the Mouse";
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            ((Label)sender).Content = e.Data.GetData(DataFormats.Text);
        }

        private void Label_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}
