using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Test_OpenFileDialog
{
//打开文件对话框

    public static class Test
    {
        private static void Normal()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
            }

            //Title：通过修改对象的Title属性，可以更改对话框的标题。
            //
            //InitialDirectory：我们还可以通过设置InitialDirectory属性来使打开文件对话框打开时在一个设置好的默认路径上。它的默认值是一个空字符串，表示用户的“我的文档”目录，第一次在应用程序中使用这个对话框时，就显示“我的文档”目录下的文件，第二次再打开对话框时，显示的目录就与上一次打开的文件所在的目录相同。
            //
            //Filter：设置文件过滤器用于打开文件对话框中显示特定类型的文件。
            //
            //ofd.Filter = "Text Document(*.txt)|*.txt|All Files|*.*|我要显示的文件类
            //
            //型(*.exe) | *.exe";
            //
            //格式是：提示1 | 类型1 | 提示2 | 类型2…
            //
            //过滤器前后不允许有空格。
            //
            //FilterIndex：指定列表框中的默认选项。
            //
            //ValidateNames：验证用户输入是否是一个有效的Windows文件名。
            //
            //CheckPathExists：验证路径有效性。
            //
            //CheckFileExists：验证文件有效性。
            //
            //Multiselect：可以使打开文件对话框打开多个文件。
        }

        private static void WithHelp()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.ShowHelp = true;
            ofd.HelpRequest += new EventHandler((object sender, EventArgs e) => {
                MessageBox.Show("我自己定义的帮助信息：");
            });
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
            }

            //ShowHelp：出现一个帮助按钮，自定义帮助信息。通过HelpRequest事件添加一个处理程序。
        }

        public static void run()
        {

        }
    }
}
