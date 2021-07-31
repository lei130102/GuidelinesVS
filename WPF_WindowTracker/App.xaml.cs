using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_WindowTracker
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //假设希望跟踪应用程序使用的所有文档窗口。为此，可在自定义应用程序类中创建专门的集合。下面是使用泛型列表集合保存一组自定义窗口对象的示例

        private List<Document> documents = new List<Document>();

        public List<Document> Documents
        {
            get { return documents; }
            set { documents = value; }
        }

        //现在，当创建新文档时，只需要记住将其添加到Documents集合中即可。下面是响应按钮单击事件的事件处理程序，该事件处理程序完成了所需的工作：
        //private void cmdCreate_Click(object sender, RoutedEventArgs e)
        //{
        //    Document doc = new Document();
        //    doc.Owner = this;
        //    doc.Show();
        //    ((App)Application.Current).Documents.Add(doc);
        //}
        //同样，也可在Document类中响应Window.Loaded这类事件，以确保当创建文档对象时，总会在Documents集合中注册该文档对象
        //(注意：这段代码设置了Window.Owner属性，使所有文档窗口都在创建他们的主窗口上显示。)
        
        //现在，可在代码的其他任何地方使用集合来遍历所有文档，并使用公有成员，在本例中，Document类包含用于更新显示的自定义方法SetContent()：
        //private void cmdUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    foreach(Document doc in ((App)Application.Current).Documents)
        //    {
        //        doc.SetContent("Refreshed at" + DateTime.Now.ToLongTimeString() + ".");
        //    }
        //}


        //总结：
        //这种方式比使用Application.Current.Windows属性要好，因为是强类型，只包含Document窗口(而不是包含应用程序中所有窗口的集合)
        //通过这种方式还可使用另一种更有用的方式对窗口进行分类。例如，可使用字典集合，通过键名更方便地查找文档。在基于文档的应用程序中，
        //可通过文件名来索引集合中的窗口
        //(当在窗口之间进行交互时，不要忘记面向对象的特点。始终使用为窗口类添加的自定义方法、属性和事件层)。永远不要直接向代码的其他部分
        //公开窗体的字段或控件。如果这么做，那么很快就会受到紧耦合接口的困扰
        
    }
}
