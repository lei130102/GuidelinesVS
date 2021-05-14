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

namespace WPF_Dialog
{
    /// <summary>
    /// ShowCustomDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ShowCustomDialog : Window
    {
        public ShowCustomDialog()
        {
            InitializeComponent();
        }

        private void btnEnterName_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CustomDialog("Please enter your name:", "John Doe");
            //使用ShowDialog()方法来显示它 - 你应该使用ShowDialog()方法而不是Show()用于模态对话
            //如果对话框的返回值为true，表示用户通过单击或按Enter键激活了Ok按钮，结果将分配给名称Label。 这就是它的全部了！
            if(dlg.ShowDialog() == true)
            {
                lblName.Text = dlg.Answer;
            }
        }
    }
}
