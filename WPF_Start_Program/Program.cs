using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Start_Program
{
    //注意，右击工程->属性->应用程序->启动对象设为Program
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            MainWindow window = new MainWindow();
            app.Run(window);
        }
    }
}
