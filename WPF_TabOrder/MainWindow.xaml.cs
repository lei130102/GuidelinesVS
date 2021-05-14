using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

//为让控件能接受焦点，必须将Focusable属性设置为true，这是所有控件的默认值

//Focusable属性是在UIElement类中定义的，这意味着其他非控件元素也可以获得焦点。对于非控件类，Focusable属性偶人设置为false，但也可以设置为true

namespace WPF_TabOrder
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// tbFocus虽然设置TabIndex="0"，但仍需要调用tbFocus.Focus()才可以
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            tbFocus.Focus();
        }
    }
}
