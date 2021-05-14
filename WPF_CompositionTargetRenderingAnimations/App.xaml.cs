using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CompositionTargetRenderingAnimations
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window w = new Window();
            w.Content = new SampleViewer();
            w.Show();

            base.OnStartup(e);
        }
    }
}
