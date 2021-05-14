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

namespace WPF_RoutedEvent.AttachedRoutedEventExample
{
    /// <summary>
    /// StackPanel_Click.xaml 的交互逻辑
    /// </summary>
    public partial class StackPanel_Click : Window
    {
        public StackPanel_Click()
        {
            InitializeComponent();

            stackPanel.AddHandler(Button.ClickEvent, new RoutedEventHandler(StackPanel_Click_1));
        }

        private void StackPanel_Click_1(object sender, RoutedEventArgs e)
        {
            //方式一：
            if (sender == cmd1)
            {
                //...
            }
            else if (sender == cmd2)
            { 
                //...
            }
            else if(sender == cmd3)
            {
                //...
            }


            //方式二：
            object tag = ((FrameworkElement)sender).Tag;
            //...
        }
    }
}
