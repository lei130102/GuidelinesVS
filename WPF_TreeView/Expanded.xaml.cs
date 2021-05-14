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

namespace WPF_TreeView
{
    /// <summary>
    /// Expanded.xaml 的交互逻辑
    /// </summary>
    public partial class Expanded : Window
    {
        public Expanded()
        {
            InitializeComponent();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach(DriveInfo driveInfo in drives)
            {
                trvStructure.Items.Add(CreateTreeItem(driveInfo));
            }
        }

        private void trvStructure_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                DirectoryInfo expandedDir = null;
                if(item.Tag is DriveInfo)
                {
                    expandedDir = (item.Tag as DriveInfo).RootDirectory;
                }
                if(item.Tag is DirectoryInfo)
                {
                    expandedDir = (item.Tag as DirectoryInfo);
                }

                try
                {
                    foreach(DirectoryInfo subDir in expandedDir.GetDirectories())
                    {
                        item.Items.Add(CreateTreeItem(subDir));
                    }
                }
                catch
                { }
            }
        }

        private TreeViewItem CreateTreeItem(object obj)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = obj.ToString();
            item.Tag = obj;
            item.Items.Add("Loading...");

            return item;
        }
    }
}

//XAML非常简单，只需要关注一个细节：我们订阅TreeViewItem的Expanded事件的方式。 请注意，这确实是TreeViewItem而不是TreeView本身，因为是冒泡事件，我们能够在一个地方捕获整个TreeView，而不必为我们添加到树中的每个项目(item)订阅它。 每次展开项目(item)时都会调用此事件，我们需要按需加载其子项目(item)。

//在后置代码中，我们首先将计算机上找到的每个驱动器添加到TreeView控件中。 我们将DriveInfo实例分配给Tag属性，以便稍后检索它。 请注意，我们通过调用自定义方法CreateTreeItem()来创建TreeViewItem，因为我们可以在以后动态添加子文件夹时使用完全相同的方法。 请注意，在此方法中，我们如何将子项添加到Items集合中，其形式为文本是“Loading...”的字符串。

//接下来是TreeViewItem_Expanded事件。 如前所述，每次展开TreeView项时都会引发此事件，因此我们要做的第一件事就是通过检查子项当前是否只包含一个项（并且是字符串）来检查是否已加载此项。 - 如果是这样，我们找到了“Loading...”子项，这意味着我们现在应该加载实际内容并用它替换占位符项。

//我们现在使用项目Tag属性来获取对当前项目所代表的DriveInfo或DirectoryInfo实例的引用，然后我们再次使用CreateTreeItem()方法获取我们添加到所单击项目的子目录列表。 请注意，我们添加每个子文件夹的循环位于try..catch块中 - 这很重要，因为某些路径可能无法访问，通常是出于安全原因。 您可以获取异常并使用它以某种方式在界面中反映这一点。


