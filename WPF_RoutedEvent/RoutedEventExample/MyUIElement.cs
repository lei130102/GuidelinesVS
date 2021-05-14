using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*

DispatcherObject(抽象类)
       |
 DependencyObject
       |
     Visual(抽象类)
       |
   UIElement
       |
  FrameworkElement

 */

namespace WPF_RoutedEvent.RoutedEventExample
{
    /// <summary>
    /// 路由事件需要有.NET事件封装器，而.NET事件封装器需要调用UIElement.AddHandler和UIElement.RemoveHandler非静态成员函数，所以路由事件所属对象类型必须派生自UIElement或其子类
    /// </summary>
    public class MyUIElement : UIElement
    {
        #region MyCustomEvent路由事件
        /// <summary>
        /// 1.定义路由事件MyCustomEvent
        /// 要求：
        /// public + static + readonly + RoutedEvent类型字段 + 字段名以Event结尾(这是约定)
        ///
        /// 2.注册路由事件
        /// 定义处注册或者在静态构造函数中注册
        /// </summary>
        public static readonly RoutedEvent MyCustomEvent =
            EventManager.RegisterRoutedEvent(
                "MyCustom"                          //事件名
                , RoutingStrategy.Bubble            //路由策略(路由类型)
                , typeof(RoutedEventHandler)        //定义事件处理方法的委托     public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);    RoutedEventArgs：在WPF中，如果事件不需要传递任何额外细节，可使用RoutedEventArgs类，该类包含了有关如何传递事件的一些细节，如果需要传递细节，那么使用他的派生类(已定义的或自定义的)
                , typeof(MyUIElement));             //拥有该事件的类型

        public event RoutedEventHandler MyCustom
        {
            add
            {
                AddHandler(MyUIElement.MyCustomEvent, value);
            }
            remove
            {
                RemoveHandler(MyUIElement.MyCustomEvent, value);
            }
        }
        #endregion
    }

    /// <summary>
    /// 在MySharedUIElement类，共享MyUIElement定义的路由事件MyCustomEvent
    ///
    /// (如果MySharedUIElement派生自MyUIElement，那么直接重用MyCustom即可)
    /// </summary>
    public class MySharedUIElement : UIElement
    {
        public static readonly RoutedEvent MyCustomEvent =
            MyUIElement.MyCustomEvent.AddOwner(typeof(MySharedUIElement));

        public event RoutedEventHandler MyCustom
        {
            add
            {
                AddHandler(MySharedUIElement.MyCustomEvent, value);
            }
            remove
            {
                RemoveHandler(MySharedUIElement.MyCustomEvent, value);
            }
        }
    }

    ////引发路由事件

    //注意路由事件不是通过.NET事件引发的，比如上面的MyCustom，而是使用UIElement.RaiseEvent()方法触发的

    //RoutedEventArgs e = new RoutedEventArgs(MyUIElement.MyCustomEvent, this);
    //RaiseEvent(e);

    //RaiseEvent()方法负责为每个已经通过AddHandler()方法注册(1.直接调用public的AddHandler。2.使用.NET事件封装器)的调用程序引发事件




    //为同一事件多次连接相同的事件处理程序，在技术角度看是可行的。这通常是编码错误的结果(这种情况下，事件处理程序会被触发多次)。如果试图删除已经连接了两次的
    //事件处理程序，事件仍会触发事件处理程序，但只触发一次




    //路由策略(RoutingStrategy)

    //1.直接路由事件(Direct)
    //不传递给其他元素
    //比如MouseEnter事件(当鼠标指针移到元素上时发生)是直接路由事件

    //2.冒泡路由事件(Bubble)
    //在包含层次中向上传递
    //窗口是整个层次中的顶级元素，并且是事件冒泡顺序的最后一站
    //比如MouseDown和MouseUp是冒泡路由事件。(每个继承自UIElement的类都提供MouseDown事件和MouseUp事件)该事件首先由被单击的元素引发，接下来被该元素的父元素引发，然后被父元素的父元素引发，依次类推，直到WPF到达元素树的顶部为止

    //3.隧道路由事件(Tunnel)   
    //在包含层次中向下传递
    //隧道路由事件在事件到达恰当的控件之前为预览事件(甚至终止事件)提供了机会
    //比如PreviewMouseDown和PreviewMouseUp事件是隧道路由事件

    //冒泡路由事件和隧道路由事件关系：

    //1.隧道路由事件都以Preview开头，WPF通常成对地定义冒泡路由事件和隧道路由事件
    //(但是没有明显的方式区分直接路由事件和冒泡路由事件)

    //2.隧道路由事件总是在冒泡路由事件之前被触发

    //3.如果将隧道路由事件标记为已处理过，那就不会发生冒泡路由事件，这是因为两个事件共享RoutedEventArgs类的同一个实例
    //(如果准备将隧道路由事件标记为处理过，务必要谨慎从事。根据编写控件的方式，这有可能阻止控件处理自己的事件(相关的冒泡路由事件)，
    //从而阻止执行某些任务或阻止更新控件自身的状态)
}
