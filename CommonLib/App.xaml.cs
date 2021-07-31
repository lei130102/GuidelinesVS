using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommonLib
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 将值锁定在 0<= <=1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double Fix(double value)
        {
            if(value < 0)
            {
                value = Math.Abs(value);
            }

            if(value > 1)
            {
                value = value % 1;
            }

            return value;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            double value1 = Fix(234.5);
            double value2 = Fix(24);
            double value3 = Fix(-234.5);
            double value4 = Fix(-24);







            //MainWindow window = new MainWindow();
            //window.Show();

            Test_FolderBrowserDialog.Test.run();
        }
    }
}
