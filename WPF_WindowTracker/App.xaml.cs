using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//访问当前Application对象

//通过静态的Application.Current属性，可在应用程序的任何位置获取当前应用程序实例，从而在窗口之间进行基本交互，因为任何窗口都可以访问
//当前Application对象，并通过Application对象获取主窗口的引用

//Window main = Application.Current.MainWindow;
//MessageBox.Show("The main window is " + main.Title);

//当然，如果希望访问在自定义主窗口类中添加的任意方法、属性或者事件，需要将窗口对象转换为正确类型。

//如果主窗口是自定义类MainWindow的实例
//MainWindow main = (MainWindow)Application.Current.MainWindow;
//main.DoSomething();

//在窗口中还可以检查Application.Windows集合的内容，该集合提供了所有当前打开窗口的引用

//foreach(Window window in Application.Current.Windows)
//{
//    MessageBox.Show(window.Title + " is open.");
//}

//(注意，当窗口(包括主窗口)显示时，会将他们添加到Windows集合中；而当窗口关闭时，会从Windows集合中移除相应的窗口。因此，窗口在集合中
//的位置可能改变，不能假定在某个特定位置可以找到特定窗口对象)





//实际上，大多数应用程序通常使用一种更具结构化特点的方式在窗口之间进行交互。如果有几个长时间运行的窗口同时打开，并且他们之间需要已某种方式
//进行通信，在自定义应用程序类中保存这些窗口的引用可能更有意义。这样，总可以找到所需的窗口。与此类似，如果有基于文档的应用程序，那么可选择
//创建跟踪文档窗口的集合，而不跟踪其他内容(见下面示例)

//自定义应用程序类是放置响应不同应用程序事件的代码的好地方。应用程序类还可以很好地达到另一个目的：保存重要窗口的引用，使一个窗口可访问另一个窗口
//(当有非模态窗口存在很长一段时间并可在其他不同的类(不仅是创建窗口的类)中被访问时，这一技术很有用。如果只是显示模态对话框作为应用程序的一部分，
//使用这种技术显得有些小题大做。在这种情况下，窗口不会存在很长时间，而且创建窗口的代码也是唯一需要访问这一窗口的代码)

namespace WPF_WindowTracker
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //这种方式比使用Application.Current.Windows属性要好，因为是强类型，只包含Document窗口(而不是包含应用程序中所有窗口的集合)
        //通过这种方式还可使用另一种更有用的方式对窗口进行分类。例如，可使用字典集合，通过键名更方便地查找文档。在基于文档的应用程序中，
        //可通过文件名来索引集合中的窗口
        //(当在窗口之间进行交互时，不要忘记面向对象的特点。始终使用为窗口类添加的自定义方法、属性和事件层)。永远不要直接向代码的其他部分
        //公开窗体的字段或控件。如果这么做，那么很快就会受到紧耦合接口的困扰
        private List<Document> documents = new List<Document>();

        public List<Document> Documents
        {
            get { return documents; }
            set { documents = value; }
        }
    }
}
