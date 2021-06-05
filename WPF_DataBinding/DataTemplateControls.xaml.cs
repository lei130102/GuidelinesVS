using System;
using System.Collections.Generic;
using System.Data;
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

namespace WPF_DataBinding
{
    /// <summary>
    /// DataTemplateControls.xaml 的交互逻辑
    /// </summary>
    public partial class DataTemplateControls : Window
    {
        public DataTemplateControls()
        {
            InitializeComponent();

            lstCategories.ItemsSource = App.StoreDbDataSet.GetCategoriesAndProducts().Tables["Categories"].DefaultView;
        }

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            DataRowView row = (DataRowView)cmd.Tag;
            lstCategories.SelectedItem = row;

            // Alternate selection approach.
            //ListBoxItem item = (ListBoxItem)lstCategories.ItemContainerGenerator.ContainerFromItem(row);
            //item.IsSelected = true;

            MessageBox.Show("You chose category #" + row["CategoryID"].ToString() + ": " + (string)row["CategoryName"]);
        }
    }
}
