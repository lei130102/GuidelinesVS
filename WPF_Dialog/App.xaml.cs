using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//视窗(winform)窗体自带许多对话框是我们至今不曾谈及的，因为一个简单的理由，它们是不存在WPF里的。它们之中最重要的一个肯定是能够让用户从文件系统里选择一个文件夹的FolderBrowserDialog，其他不存在于WPF里的对话框包括ColorDialog，FontDialog，PrintPreviewDialog以及PageSetupDialog.

//对于WPF开发人员来说，这可能是一个真正的问题，因为重新实现这些对话框将是一项艰巨的任务。幸运的是，只需引用System.Windows.Forms程序集就可以混合使用WPF和WinForms，但由于WPF对颜色和对话框使用不同的基本类型，因此这并不总是可行的解决方案。然而，如果你只需要FolderBrowserDialog，这是一个简单的解决方案，因为它只处理文件夹路径作为简单的字符串，但是一些纯粹主义者会认为混合WPF和WinForms永远不会有办法。

//如果你不想自己重新发明轮子，更好的方法可能是使用其他开发人员创建的一些工作。以下是一些文章链接，它提供了一些缺少对话框的解决方案：
//- WPF的FontDialog替代方案   https://www.codeproject.com/Articles/368070/A-WPF-Font-Picker-with-Color
//- WPF的ColorDialog替代方案  https://www.codeproject.com/Articles/33001/WPF-A-Simple-Color-Picker-With-Preview

namespace WPF_Dialog
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
