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

namespace WPF_Button
{
    /// <summary>
    /// Command.xaml 的交互逻辑
    /// </summary>
    public partial class Command : Window
    {
        public Command()
        {
            InitializeComponent();
        }

        private ICommand _customCommand;
        public ICommand CustomCommand
        {
            get
            {
                if(_customCommand == null)
                {
                    _customCommand = new RelayCommand(
                        obj => this.Execute(),
                        obj => this.CanExecute()
                        );
                }
                return _customCommand;
            }
        }

        private bool CanExecute()
        {
            //...
            return true;
        }

        private void Execute()
        {
            MessageBox.Show("clicked");
        }
    }
}
