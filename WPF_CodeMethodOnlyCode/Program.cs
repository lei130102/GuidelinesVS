using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CodeMethodOnlyCode
{
    //static class Program
    //{
    //    /// <summary>
    //    /// 应用程序的主入口点。
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Form1());


    //    }
    //}

    public class App : Application
    {
        [STAThread()]
        static void Main()
        {
            App app = new App();
            app.MainWindow = new MyWindow();
            app.MainWindow.ShowDialog();
        }
    }
}
