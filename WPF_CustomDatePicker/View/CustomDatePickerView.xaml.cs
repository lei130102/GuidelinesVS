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
using System.Windows.Shapes;
using WPF_CustomDatePicker.Utility;

namespace WPF_CustomDatePicker.View
{
    /// <summary>
    /// CustomDatePickerView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IPluginView))]
    [ExportMetadata("Type", new string[] { "CustomDatePickerView" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CustomDatePickerView : Window, IPluginView
    {
        public CustomDatePickerView()
        {
            InitializeComponent();
        }
    }
}
