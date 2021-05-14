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

namespace WPF_Dialog
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        private void btnSimpleMessageBox_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!");
        }

        private void btnMessageBoxWithTitle_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!", "My App");
        }

        private void btnMessageBoxWithButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("This MessageBox has extra options.\n\nHello,world?", "My App", MessageBoxButton.YesNoCancel);
        }

        private void btnMessageBoxWithResponse_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to greet the world with a \"Hello,world\"?", "My App", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    System.Windows.MessageBox.Show("Hello to you too!", "My App");
                    break;
                case MessageBoxResult.No:
                    System.Windows.MessageBox.Show("Oh well,too bad!", "My App");
                    break;
                case MessageBoxResult.Cancel:
                    System.Windows.MessageBox.Show("Nevermind then...", "My App");
                    break;
            }
        }

        private void btnMessageBoxWithIcon_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!", "My App", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnMessageBoxWithDefaultChoice_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }
    }
}
