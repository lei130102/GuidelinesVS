using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//单实例应用程序

//对于单实例应用程序，WPF本身并未提供自带的解决方法，变通地方法：
//使用Windows窗体提供的内置支持，这一内置支持最初是用于Visual Basic应用程序的。这种方法在后台处理杂乱的问题
//(WPF的未来版本最终会支持单实例应用程序。目前，这种变通方法提供了相同的功能，只不过需要做的工作稍微多一些)

//如何使用位Window窗体和Visual Basic设计的这一特性，管理使用C#开发的WPF应用程序呢？
//本质上，旧式应用程序类充当了WPF应用程序类的封装器。当启动应用程序时，将创建旧式应用程序类，旧式应用程序类接着创建WPF应用程序类。旧式应用程序类
//处理实例管理问题，而WPF应用程序类处理真正地应用程序





//下面的SingleInstanceApplicationWrapper类、WpfApp类和Startup类——构成了单实例WPF应用程序的基础

namespace WPF_SingleInstanceApplication
{
    /// <summary>
    /// 注意要在项目配置中设置启动对象为 WPF_SingleInstanceApplication.Startup，调试-命令行参数中设置“sample1.testDoc”
    /// </summary>
    public class Startup
    {
        //因为应用程序需要在App类之前创建SingleInstanceApplicationWrapper类，所以应用程序必须使用传统的Main()方法来启动，而不能使用App.xaml文件
        [STAThread]
        public static void Main(string[] args)
        {
            SingleInstanceApplicationWrapper wrapper = new SingleInstanceApplicationWrapper();
            wrapper.Run(args);
        }
    }

    public class SingleInstanceApplicationWrapper :
        Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase   //依赖Microsoft.VisualBasic引用
    {
        public SingleInstanceApplicationWrapper()
        {
            // Enable single-instance mode.
            //启用单实例应用程序，在构造函数中将该属性设置为true
            this.IsSingleInstance = true;
        }

        // Create the WPF application class.
        private WpfApp app;

        //当应用程序启动时触发的OnStartup()方法，此时重写该方法并创建WPF应用程序对象
        protected override bool OnStartup(
            Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            string extension = ".testDoc";
            string title = "WPF_SingleInstanceApplication";
            string extensionDescription = "A Test Document";

            //Uncomment this line to create the file registration.
            //In Windows Vista, you'll need to run the application as an administrator.

            //FileRegistrationHelper.SetFileAssociation(extension, title + "." + extensionDescription);

            app = new WpfApp();
            app.Run();

            return false;
        }

        // Direct multiple instance
        //当另一个应用程序实例启动时触发的OnStartupNextInstance()方法。该方法提供了访问命令行参数的功能。此时，可调用WPF应用程序类中的方法来显示新的窗口，
        //但不创建另一个应用程序对象
        protected override void OnStartupNextInstance(Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);

            if(eventArgs.CommandLine.Count > 0)
            {
                app.ShowDocument(eventArgs.CommandLine[0]);
            }
        }
    }

    public class WpfApp : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load the main window.
            DocumentList list = new DocumentList();
            this.MainWindow = list;
            list.Show();

            // Load the document that was specified as an argument.
            if(e.Args.Length > 0)
            {
                ShowDocument(e.Args[0]);
            }
        }

        // An ObservableCollection is a List that provides notification
        // when items are added, deleted, or removed. It's preferred for data binding.
        private ObservableCollection<DocumentReference> documents = new ObservableCollection<DocumentReference>();
        public ObservableCollection<DocumentReference> Documents
        {
            get { return documents; }
            set { documents = value; }
        }

        public void ShowDocument(string filename)
        {
            try
            {
                Document doc = new Document();
                DocumentReference docRef = new DocumentReference(doc, filename);
                doc.LoadFile(docRef);
                doc.Owner = this.MainWindow;
                doc.Show();
                doc.Activate();
                Documents.Add(docRef);
            }
            catch
            {
                MessageBox.Show("Could not load document.");
            }
        }
    }
}
