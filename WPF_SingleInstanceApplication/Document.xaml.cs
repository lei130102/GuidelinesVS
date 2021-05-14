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

namespace WPF_SingleInstanceApplication
{
    /// <summary>
    /// Document.xaml 的交互逻辑
    /// </summary>
    public partial class Document : Window
    {
        public Document()
        {
            InitializeComponent();
        }

        private DocumentReference docRef;

        public void LoadFile(DocumentReference docRef)
        {
            this.docRef = docRef;

            //设置当前Window对象的Content属性，该属性决定在窗口的客户区域显示什么内容。更有趣之处是，WPF窗口实际上是一种内容控件
            //(这意味着他们继承自ContentControl类)。因此，他们可包含和显示单个对象。这个对象可以是字符串、控件或可包含多个控件
            //的更有用的面板
            this.Content = File.ReadAllText(docRef.Name);
            this.Title = docRef.Name;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            ((WpfApp)Application.Current).Documents.Remove(docRef);
        }
    }
}
