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
    /// SimpleDocument.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleDocument : Window
    {
        private bool isDirty = false;

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDirty = true;
        }

        public SimpleDocument()
        {
            InitializeComponent();

            //Create bindings.

            CommandBinding binding;

            binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += NewCommand;
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += OpenCommand;
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += SaveCommand_Executed;
            binding.CanExecute += SaveCommand_CanExecute;
            this.CommandBindings.Add(binding);
        }

        #region New
        private void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered with " + e.Source.ToString());
            isDirty = false;
        }
        #endregion

        #region Open
        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = false;
        }
        #endregion

        #region Save
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save command triggered with " + e.Source.ToString());
            isDirty = false;
        }
        #endregion
    }
}
