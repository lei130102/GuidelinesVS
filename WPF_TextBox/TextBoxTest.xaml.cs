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

namespace WPF_TextBox
{
    /// <summary>
    /// TextBoxTest.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxTest : Window
    {
        public TextBoxTest()
        {
            InitializeComponent();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //SelectionStart，它给出了当前光标位置或是否有选择：它从什么位置开始。
            //SelectionLength，它给出了当前选择的长度，如果有的话。 否则它将返回0。
            //SelectedText，如果有选择，它会给我们当前选择的字符串。 否则返回一个空字符串。

            //修改选择
            //所有这些属性都是可读的和可写的，这意味着您也可以修改它们。例如，您可以设置SelectionStart和SelectionLength属性以选择
            //自定义文本范围，或者可以使用SelectedText属性插入和选择字符串。请记住，文本框必须具有焦点，例如首先调用Focus()方法，
            //以便工作。

            TextBox textBox = sender as TextBox;
            txtStatus.Text = "Selection starts at character #" + textBox.SelectionStart + Environment.NewLine;
            txtStatus.Text += "Selection is " + textBox.SelectionLength + " character(s) long" + Environment.NewLine;
            txtStatus.Text += "Selected text: '" + textBox.SelectedText + "'";
        }
    }
}
