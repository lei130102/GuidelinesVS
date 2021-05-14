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
using System.Windows.Shapes;

namespace WPF_Command
{
    /// <summary>
    /// TwoDocument.xaml 的交互逻辑
    /// </summary>
    public partial class TwoDocument : Window
    {
        public TwoDocument()
        {
            InitializeComponent();
        }

        private Dictionary<Object, bool> isDirty = new Dictionary<object, bool>();

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            MessageBox.Show("About to save：" + text);
            isDirty[sender] = false;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(isDirty.ContainsKey(sender) && isDirty[sender] == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDirty[sender] = true;
        }
    }
}
