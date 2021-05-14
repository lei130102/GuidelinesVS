using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using WPF_MVVM_Basic.Utility;

namespace WPF_MVVM_Basic.View
{
    /// <summary>
    /// SecondView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IPluginView))]
    [ExportMetadata("Type", new[]{ "新窗口" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SecondView : Window, IPluginView
    {
        public SecondView()
        {
            InitializeComponent();
        }
    }
}
