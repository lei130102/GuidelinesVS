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

//WPF事件：(有的是普通.NET事件，有的是路由事件)

//1.生命周期事件

//2.输入事件(InputEventArgs，派生自RoutedEventArgs)
//2.1鼠标事件(MouseEventArgs和他的派生类)
//2.2键盘事件(KeyboardEventArgs和他的派生类)
//2.3手写笔事件(StylusEventArgs和他的派生类)
//2.4多点触控事件(TouchEventArgs和他的派生类)

//InputEventArgs类只增加了两个属性：Timestamp和Device。
//Timestamp属性提供了一个整数，指示事件何时发生的毫秒数(他所代表的实际时间并不重要，但可比较不同的时间戳值以确定哪个事件先发生。时间戳值大的事件是在更近发生的)
//Device属性返回一个对象，该对象提供与触发事件的设备相关的更多信息，设备可以是鼠标、键盘或者手写笔。这三种可能的设备由不同的类表示，所有这些类都继承自抽象类System.Windows.Input.InputDevice

namespace WPF_RoutedEvent.WPF_Event
{
    /// <summary>
    /// Event.xaml 的交互逻辑
    /// </summary>
    public partial class Event : Window
    {
        public Event()
        {
            InitializeComponent();
        }
    }
}
