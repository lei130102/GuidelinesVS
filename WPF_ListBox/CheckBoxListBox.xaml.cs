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
    public partial class CheckBoxListBox : Window
    {
        public CheckBoxListBox()
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



              
            //如果希望查找当前选择的项，可直接从SelectedItem或SelectedItems属性中读取
            //如果希望确定哪些项(如果存在的话)被取消选中，可使用SelectionChangedEventArgs对象的RemovedItems属性
            //可通过AddedItems属性了解哪些项被添加到了选中的项中。在单项选择模式下，无论何时选项发生变化，总有一项被选中并总有一项被取消选中。在多项选择或扩展选择模式下，情况就未必如此了
            if (lst.SelectedItem == null) return;

            txtSelection.Text = String.Format("You chose item at position {0}.\r\nChecked state is {1}.", lst.SelectedIndex, ((CheckBox)lst.SelectedItem).IsChecked);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //遍历选项集合以确定哪一项被选中了
            //(对于使用复选框的多项选择列表，可以编写类似的代码来遍历选中项的集合)
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
