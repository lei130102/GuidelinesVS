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

namespace WPF_DataBinding
{
    /// <summary>
    /// ComboBoxSelectionBox.xaml 的交互逻辑
    /// </summary>
    public partial class ComboBoxSelectionBox : Window
    {
        public ComboBoxSelectionBox()
        {
            InitializeComponent();

            lstProducts.ItemsSource = App.StoreDb.GetProducts();
            // Select the first item.
            lstProducts.SelectedIndex = 0;
        }

        private void txtTextSearchPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Re-select the so the new TextSearch.TextPath is evaluated.
            int currentIndex = lstProducts.SelectedIndex;
            lstProducts.SelectedIndex = -1;
            lstProducts.SelectedIndex = currentIndex;
        }
    }
}
