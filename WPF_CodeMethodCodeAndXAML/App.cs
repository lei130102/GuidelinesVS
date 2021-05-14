using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CodeMethodCodeAndXAML
{
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
