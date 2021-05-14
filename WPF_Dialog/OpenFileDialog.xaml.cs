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
    /// OpenFileDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OpenFileDialog : Window
    {
        public OpenFileDialog()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Microsoft.Win32命名空间中的标准打开对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            //过滤
            openFileDialog.Filter = "Text files (*.txt)|*.txt|Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            //设置初始目录
            openFileDialog.InitialDirectory = @"C:\temp\";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//如果要在Windows上使用某个特殊文件夹，例如在桌面，我的文档或程序文件目录中，您必须特别小心，因为这些可能因Windows的每个版本而异，也取决于登录的用户。但.NET框架可以帮助您，只需使用环境类及其成员处理特殊文件夹：

            ////单个文件
            //openFileDialog.Multiselect = false;
            //if (openFileDialog.ShowDialog() == true)//ShowDialog()将返回一个可以为空的布尔值，这意味着它可以是false，true或null。如果用户选择文件并按“打开”，则结果为True，在这种情况下，我们尝试将文件加载到TextBox控件中。我们使用OpenFileDialog的FileName属性获取所选文件的完整路径。
            //{
            //    txtEditor.Text = File.ReadAllText(openFileDialog.FileName);//FileName
            //}
            //或者
            //多个文件
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)//ShowDialog()将返回一个可以为空的布尔值，这意味着它可以是false，true或null。如果用户选择文件并按“打开”，则结果为True，在这种情况下，我们尝试将文件加载到TextBox控件中。我们使用OpenFileDialog的FileName属性获取所选文件的完整路径。
            {
                foreach (string filename in openFileDialog.FileNames)//FileNames
                {
                    txtOpenFile.Text += System.IO.Path.GetFileName(filename) + "\n";
                }
            }
        }
    }
}
