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

//键盘事件

//所有元素的键盘事件(按顺序)
//PreviewKeyDown              隧道                  当按下一个键时发生
//KeyDown                     冒泡                  当按下一个键时发生
//PreviewTextInput            隧道                  当按键完成并且元素正在接收文本输入时发生。对于那些不会产生文本“输入”的按键(如Ctrl键、Shift键、Backspace键、方向键和功能键等)，不会引发该事件
//TextInput                   冒泡                  当按键完成并且元素正在接收文本输入时发生。对于那些不会产生文本的按键，不会引发该事件
//PreviewKeyUp                隧道                  当释放一个按键时发生
//KeyUp                       冒泡                  当释放一个按键时发生

//一些控件可能会挂起这些事件中的某些事件，从而可执行自己更特殊的键盘处理：
//TextBox控件：
//1.TextBox控件挂起了TextInput事件（仍可以使用隧道路由事件PreviewTextInput），添加了TextChanged的新事件，在按键导致文本框中的文本发生改变之后会立即引发该事件
//2.对于一些按键(如方向键)，挂起了KeyDown事件（仍可以使用隧道路由事件PreviewKeyDown）
//3.对于一些按键(如空格键)，不会触发PreviewTextInput事件

namespace WPF_RoutedEvent.WPF_Event
{
    /// <summary>
    /// KeyEvent.xaml 的交互逻辑
    /// </summary>
    public partial class KeyEvent : Window
    {
        public KeyEvent()
        {
            InitializeComponent();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            lstMessages.Items.Clear();
        }

        /// <summary>
        /// PreviewKeyDown、KeyDown、PreviewKeyUp和KeyUp事件可以使用同一个事件处理方法，因为都是通过KeyEventArgs提供信息
        /// 
        /// 注意，对于一些按键(如方向键)，TextBox挂起了KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_Event(object sender, KeyEventArgs e)
        {
            //根据Windows键盘的设置，持续按下一个键一段时间，会重复引发按键事件。如果希望忽略这些重复的Shift键，可以通过检查
            //KeyEventArgs.IsRepeat属性，确定按键是不是因为按住键导致的结果
            if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;

            string message = //"At：" + e.Timestamp.ToString() +
                "Event：" + e.RoutedEvent + " " +                               //KeyEventArgs.RoutedEvent    区分是哪个路由事件
                " Key：" + e.Key + " " +                                        //KeyEventArgs.Key            按下或释放的键

                //Key枚举区分数字键盘和普通键盘字母以上的数字键，这意味着根据按下数字9的方式，可能得到值Key.D9或者Key.NumPad9
                //可以使用KeyConverter类将Key值转换为更有用的字符串
                //KeyConverter converter = new KeyConverter();
                //string key = Converter.ConvertToString(e.Key);
                //比如返回"9"
                //如果使用Key.ToString()那么返回"D9"或者"NumPad9"
                //但缺陷是对于不会产生文本输入的按键会得到更长一点的文本，比如"Backspace"


                //当发生按键事件时，经常需要知道更多信息，而不仅要知道按下的是哪个键。而且确定其他键是否同时被按下也非常重要，比如修饰键

                //对于键盘事件(PreviewKeyDown、KeyDown、PreviewKeyUp和KeyUp)，获取这些信息比较容易，可以使用KeyboardDevice类

                //e.KeyboardDevice.FocusedElement   具有键盘焦点的元素
                //e.KeyboardDevice.Modifiers        按下的所有修饰键(修饰键包括Shift、Ctrl和Alt键，并且可以使用位逻辑来检查他们的状态)

                " Modifiers：" + e.KeyboardDevice.Modifiers.ToString();
            if((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                message += " You held the Control key.";
            }

            lstMessages.Items.Add(message);

                //KeyboardDevice类的简便方法：
                //IsKeyDown：当事件发生时，通知是否按下了该键
                //IsKeyUp：当事件发生时，通知是否释放了该键
                //IsKeyToggled：当事件发生时，通知该键是否处于“打开”状态。该方法只对那些能够打开、关闭的键有意义，如Caps Lock键、Scroll Lock键以及Num Lock键
                //GetKeyStates：返回一个或多个KeyStates枚举值，指明该键当前是否被释放了、按下了或处于切换状态。该方法本质上和为同一个键同时调用IsKeyDown()方法和IsKeyToggled()方法相同

                //对于非键盘事件(PreviewKeyDown、KeyDown、PreviewKeyUp和KeyUp)，获取这些信息使用KeyBoard类
                //KeyBoard类的简便方法类似KeyboardDevice类，只是都是静态函数

                //(Keyboard类也提供了几个方法，通过这些方法可关联应用程序范围内的键盘事件处理程序，如AddKeyDownHandler()和AddKeyUpHandler()方法，然而，建议不要使用这些方法，
                //实现应用程序范围内功能的较好方法是使用WPF命令系统)


            //最好使用PreviewKeyDown、KeyDown、PreviewKeyUp和KeyUp事件编写低级的键盘处理逻辑（除了自定义控件外很少需要），并用于处理特殊的按键，如功能键
        }

        /// <summary>
        /// KeyDown事件发生后，接着发生PreviewTextInput事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string message = //"At：" + e.Timestamp.ToString() +
                "Event：" + e.RoutedEvent + " " +
                " Text：" + e.Text;                                      //TextChangedEventArgs.Text 提供了处理过的文本，是控件即将接收到的文本
            lstMessages.Items.Add(message);
        }

        /// <summary>
        /// PreviewTextInput事件发生后，接着发生TextInput事件，但TextBox挂起了TextInput事件，所以不会发生TextInput事件，但会在导致文本框中的文本发生改变之后会发生新事件TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string message =
                "Event：" + e.RoutedEvent;
            lstMessages.Items.Add(message);
        }

        private void cmdCheckShift_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                MessageBox.Show("The left Shift is held down.");
            }
            else
            {
                MessageBox.Show("The left shift is not held down.");
            }
        }

        //利用PreviewTextInput事件执行验证工作，PreviewKeyDown辅助某些按键(比如空格)不会触发PreviewTextInput事件的情况

        //注意这样方式的验证还是有问题的，如果开启输入法，那么还是会将非数字输入进去

        private void tbNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short val;
            if(!Int16.TryParse(e.Text, out val))
            {
                //Disallow non-numeric key presses.
                e.Handled = true;
            }
        }

        private void tbNum_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                //Disallow the space key, which doesn't raise a PreviewTextInput event
                e.Handled = true;
            }
        }

        //这一按键处理行为看起来比较笨拙。一个原因是TextBox控件没有提供更好的按键处理，因为WPF关注的是数据绑定，通过该
        //特性可将控件（如TextBox控件）绑定到自定义对象。当使用这种方法时，通常由绑定对象提供验证，通过异常标识错误，
        //并且非法数据会触发在用户界面中的某个位置显示的错误信息。但是，目前没有比较容易的方法将这些有用的、高级的数据
        //绑定特性应用于低级的防止用户输入非法字符的键盘处理中
    }
}

//在Windows世界中，用户每次只能使用一个控件，当前接收用户按键的控件是具有焦点的控件（关于焦点的讨论见WPF_TabOrder项目）
