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
    /// NoCommandTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class NoCommandTextBox : Window
    {
        public NoCommandTextBox()
        {
            InitializeComponent();

            txt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, SuppressCommand));

            txt.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.C, ModifierKeys.Control));
            //txt.ContextMenu = null;
        }

        private void SuppressCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }
    }
}
