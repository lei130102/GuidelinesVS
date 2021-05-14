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



//创建自定义输入对话框
//在最后几篇文章中，我们研究了使用WPF的内置对话框，但创建自己的对话框几乎同样容易。实际上，您只需要创建一个Window，将所需的控件放在其中然后显示它。

//在XAML部分中，我使用了Grid来控制控件的布局 - 这没什么特别的。 我删除了窗口的宽度和高度属性，而是将其设置为自动调整大小以匹配内容 - 这在对话框中是有意义的，因此您不必微调大小以使一切看起来都正常。 相反，使用边距和最小尺寸来确保看起来像您希望的那样，同时仍然允许用户调整对话框的大小。

//我在Window上更改的另一个属性是WindowStartupLocation。 对于这样的对话框，可能对于大多数其他非主窗口，您应该将此值更改为CenterScreen或CenterOwner，以更改窗口将出现在Windows决定的位置的默认行为，除非您手动指定它的Top和Left属性。

//还要特别注意我在对话框按钮上使用的两个属性：IsCancel和IsDefault。 IsCancel告诉WPF，如果用户单击此按钮，则Window的DialogResult应设置为false，这也将关闭窗口。 用户也可以按下键盘上的Esc键来关闭窗口，这在Windows对话框中是非常合适的。
//IsDefault属性将焦点放在Ok按钮上，如果用户按下键盘上的Enter键，则会激活此按钮。 但是，如下所述，需要在事件处理程序设置DialogResult。


namespace WPF_Dialog
{
    /// <summary>
    /// CustomDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CustomDialog : Window
    {
        public CustomDialog(string question, string defaultAnswer = "")
        {
            InitializeComponent();

            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
        }

        /// <summary>
        /// Ok按钮有一个事件处理程序，可确保在单击时将Window的DialogResult属性设置为true，向对话框的发起者返回用户的输入值。
        /// 我们没有处理取消按钮，因为当我们将IsCancel属性设置为true时，WPF会为我们处理这个，如上所述。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// 为了在显示对话框时将焦点放在TextBox上，我订阅了ContentRendered事件，我在其中选择控件中的所有文本然后给予焦点。 
        /// 如果我只是想给焦点，我可以使用Window上的FocusManager.FocusedElement附加属性，但在这种情况下，我还想选择文本，
        /// 以允许用户立即覆盖默认提供的答案（如果有的话）。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        /// <summary>
        /// 最好提供一个带有对话框返回值的属性，而不是直接从窗口外部访问控件。 如果需要，这样做您可以在返回之前修改返回值。
        /// </summary>
        public string Answer
        {
            get
            {
                return txtAnswer.Text;
            }
        }
    }
}
