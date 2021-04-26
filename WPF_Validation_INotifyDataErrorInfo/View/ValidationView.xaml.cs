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
using WPF_Validation_INotifyDataErrorInfo.Utility;

namespace WPF_Validation_INotifyDataErrorInfo.View
{
    /// <summary>
    /// ValidationView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IPluginView))]
    [ExportMetadata("Type", new string[] { "ValidationView" })]
    public partial class ValidationView : Window, IPluginView
    {
        public ValidationView()
        {
            InitializeComponent();
        }
    }
}
