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

namespace WPF_ListBox
{
    /// <summary>
    /// CheckBoxList.xaml 的交互逻辑
    /// </summary>
    public partial class CheckBoxList : Window
    {
        public CheckBoxList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 注意SelectionChanged事件的处理方法的参数是SelectionChangedEventArgs对象，但为了和Click事件的处理方法相同，可以改为参数为父类RoutedEventArgs对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Select when checkbox portion is clicked (optional)
            if(e.OriginalSource is CheckBox)
            {
                lst.SelectedItem = e.OriginalSource;
            }

            if (lst.SelectedItem == null) return;

            txtSelection.Text = String.Format("You chose item at position {0}.\r\nChecked state is {1}.", lst.SelectedIndex, ((CheckBox)lst.SelectedItem).IsChecked);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(CheckBox item in lst.Items)
            {
                if(item.IsChecked == true)
                {
                    sb.Append(item.Content + " is checked.");
                    sb.Append("\r\n");
                }
            }
            txtSelection.Text = sb.ToString();
        }
    }
}
