using System;
using System.Collections.Generic;
using System.IO;
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
    /// SaveFileDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SaveFileDialog : Window
    {
        public SaveFileDialog()
        {
            InitializeComponent();
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32命名空间中的标准保存文件对话框
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            //过滤
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            //设置初始目录
            saveFileDialog.InitialDirectory = @"C:\temp\";
            //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//如果要在Windows上使用某个特殊文件夹，例如在桌面，我的文档或程序文件目录中，您必须特别小心，因为这些可能因Windows的每个版本而异，也取决于登录的用户。但.NET框架可以帮助您，只需使用环境用于处理特殊文件夹的类及其成员：

            //默认为true，并确定SaveFileDialog是否应自动为文件名附加扩展名（如果用户省略）。扩展名将基于所选的过滤器，除非这是不可能的，在这种情况下，它将回退到DefaultExt属性（如果指定）。如果您希望应用程序能够保存没有文件扩展名的文件，则可能必须禁用此选项。
            saveFileDialog.AddExtension = true;
            //默认为true，并确定如果用户输入的文件名将导致现有文件被覆盖，SaveFileDialog是否应该要求确认。除非在非常特殊的情况下，您通常希望启用此选项。
            saveFileDialog.OverwritePrompt = true;
            //如果您想在对话框中使用自定义标题，则可以覆盖此属性。它默认为“另存为”或本地化的等效项，该属性对OpenFileDialog也有效。
            saveFileDialog.Title = "另存为";
            //默认为true，除非它被禁用，否则它将确保用户在允许用户继续之前只输入有效的Windows文件名。
            saveFileDialog.ValidateNames = true;

            if (saveFileDialog.ShowDialog() == true)//。如果它返回true，我们使用FileName属性（它将包含所选路径以及用户输入的文件名）作为将内容写入的路径。
            {
                File.WriteAllText(saveFileDialog.FileName, txtSaveFile.Text);
            }
        }
    }
}
