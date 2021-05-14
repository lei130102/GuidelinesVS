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



//UpdateSourceTrigger属性
//在上一篇文章中，我们看到了TextBox中的更改是如何不立即发送回源的。相反，只有在TextBox上丢失焦点后才更新源。此行为由名为UpdateSourceTrigger的绑定上的属性控制。它默认为值“Default”，这基本上意味着根据您绑定的属性更新源。在编写时，除了Text属性之外的所有属性在属性更改时立即更新（PropertyChanged），而当目标元素丢失焦点时（LostFocus），Text属性会更新。

//显然，Default是UpdateSourceTrigger的默认值。其他选项是PropertyChanged, LostFocus和Explicit。前两个已经描述过，而最后一个只是意味着必须手动推送更新，使用Binding上的UpdateSource调用。



namespace WPF_Binding
{
    /// <summary>
    /// UpdateSourceTrigger.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateSourceTrigger : Window
    {
        public UpdateSourceTrigger()
        {
            InitializeComponent();
        }

        private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression bindingExpression = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
        }
    }
}


//如您所见，三个文本框中的每一个现在都使用不同的UpdateSourceTrigger.

//第一个设置为Explicit，这基本上意味着除非您手动执行，否则不会更新源。出于这个原因，我在TextBox旁边添加了一个按钮，它将根据需要更新源值。在Code-behind中，您将找到Click处理程序，我们使用几行代码从目标控件获取绑定，然后在其上调用UpdateSource()方法。

//第二个TextBox使用LostFocus值，这实际上是Text绑定的默认值。这意味着每次目标控件失去焦点时都会更新源值。

//第三个也是最后一个TextBox使用PropertyChanged值，这意味着每次绑定属性更改时都会更新源值，在这种情况下，只要文本更改，它就会更新。

//尝试在您自己的计算机上运行该示例，并查看三个文本框的行为完全不同：第一个值在您单击按钮之前不会更新，第二个值在您离开TextBox之前不会更新，而第三个值会自动更新每次击键，文字更改等






//绑定的UpdateSourceTrigger属性控制将更改的值发送回源的方式和时间。 但是，由于WPF非常擅长为您控制，因此默认值应该足以满足大多数情况，在这种情况下，您将获得不断更新的UI和良好性能的最佳组合。

//对于那些需要更多控制过程的情况，这个属性肯定会有所帮助。 只需确保不会比实际需要更频繁地更新源值。 如果您想要完全控制，可以使用Explicit值，然后手动执行更新，但这确实会失去一些使用数据绑定的乐趣。
