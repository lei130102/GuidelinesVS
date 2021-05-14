using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//本地化
//这个不常用，略去

namespace WPF_LocalizableApplication
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

        }
    }
}
