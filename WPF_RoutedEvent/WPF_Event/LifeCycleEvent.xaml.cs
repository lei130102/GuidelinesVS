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

namespace WPF_RoutedEvent.WPF_Event
{
    /// <summary>
    /// LifeCycleEvent.xaml 的交互逻辑
    /// </summary>
    public partial class LifeCycleEvent : Window
    {
        public LifeCycleEvent()
        {
            InitializeComponent();
        }

        //派生自FrameworkElement类的都有这三个事件

        /// <summary>
        /// Initialized事件(普通.NET事件，并非路由事件)
        /// 当元素被实例化，并已根据XAML标记设置了元素的属性之后发生。这时元素已经初始化，但窗口的其他部分可能尚未初始化。
        /// 此外，尚未应用样式和数据绑定。这时，IsInitialized属性为true。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Initialized(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Loaded事件
        /// 当整个窗口已经初始化并应用了样式和数据绑定时，该事件发生。这是在元素被呈现之前的最后一站。这时，IsLoaded属性为true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {

        }





        
        //FrameworkElement类实现了ISupportInitialize接口
        //XAML解析器在实例化元素后立即调用ISupportInitialize.BeginInit()，然后设置所有元素的属性(并添加内容)，完成初始化后，调用ISupportInitialize.EndInit()，此时引发Initialized事件
        //(但是，如果手动创建一个元素并将它添加到窗口中，就未必使用这个接口。对于这种情况，一旦将元素添加到窗口，就会在Loaded事件之前引发Initialized事件)







        //Initialized事件和Loaded事件的触发顺序
        //Initialized事件：
        //子子窗口->子窗口->窗口
        //布局、样式和数据绑定
        //Loaded事件：
        //窗口->子窗口->子子窗口







        //如果只对执行控件的第一次初始化感兴趣，完成这项任务的最好时机是在触发Loaded事件时。通常
        //可在同一位置进行所有初始化，这个位置一般是Window.Loaded事件的事件处理程序
        //(也可以使用窗口构造函数进行初始化(在紧跟InitializeComponent()调用之后，添加自己的代码)。但使用
        //Loaded事件总是更好的选择。这是因为如果在Window类的构造函数中发生异常，就会在XAML解析器解析页面时
        //抛出该异常。因此，该异常将与InnerException属性中的原始异常一起被封装到一个没有用处的XamlParseException对象中)




        /// <summary>
        /// Unloaded事件
        /// 当元素被释放时，该事件发生，原因是包含元素的窗口被关闭或特定的元素被从窗口中删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
