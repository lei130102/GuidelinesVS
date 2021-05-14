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

//IsCancel属性
//如果将IsCancel属性设置为true，按钮就成为窗口的取消按钮。在当前窗口的任何位置如果按下Esc键，就会触发该按钮

//IsDefault属性
//如果将IsDefault属性设置为true，按钮就成为默认按钮(也就是接受按钮)。其行为取决于焦点在窗口中的当前位置。如果焦点位于某个非按钮控件中
//(如TextBox控件、RadioButton控件和CheckBox控件等)，默认按钮具有蓝色阴影，几乎像是具有焦点。如果按下Enter键，就会触发默认按钮。但如果
//焦点位于另一个按钮控件上，当前有焦点的按钮就具有蓝色阴影，而且按下Enter键会触发当前按钮而不是默认按钮

//注意，仍需为取消按钮和默认按钮编写事件处理代码，因为WPF没有提供这一行为

//可以将窗口中的同一个按钮既设置为取消按钮，又设置为默认按钮(比如About对话框中的OK按钮)

//窗口中只能有一个取消按钮和一个默认按钮。如果指定多个取消按钮，按下Esc键将把焦点移到下一个默认按钮，而不是触发他。如果设置多个默认按钮，按下Enter键
//后的行为更混乱。如果焦点在某个非按钮控件上，按下Enter键会把焦点移到下一个默认按钮。如果焦点位于一个Button控件上，按下Enter键就会触发该Button控件

//(
//IsDefaulted属性(只读)
//如果另一个控件具有焦点并且该控件不接受Enter键输入，那么对于默认按钮，IsDefaulted属性会返回true，这种情况下，按下Enter键会触发该按钮
//(例如，除非将TextBox.AcceptsReturn属性设置为true，否则TextBox控件不接受Enter键输入。当AcceptsReturn属性被设置为true的TextBox控件具有焦点时，
//默认按钮的IsDefaulted属性为false。当AcceptsReturn属性被设置为false的TextBox控件具有焦点时，默认按钮的IsDefaulted属性为true。还要更容易令人困
//惑的情况，当按钮本身具有焦点时，IsDefaulted属性返回false，尽管这时按下Enter键会触发该按钮)
//
//尽管未必希望使用IsDefaulted属性，但使用该属性确实可以编写出特定类型的样式触发器，如果不使用IsDefaulted属性，只是将它添加到普通的WPF列表中，这样常会使
//您的同事感到困惑
//)

namespace WPF_Button
{
    /// <summary>
    /// IsCancelIsDefault.xaml 的交互逻辑
    /// </summary>
    public partial class IsCancelIsDefault : Window
    {
        public IsCancelIsDefault()
        {
            InitializeComponent();
        }
    }
}
