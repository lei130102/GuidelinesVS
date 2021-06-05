using StoreDatabase;
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
    /// NavigateCollection.xaml 的交互逻辑
    /// </summary>
    public partial class NavigateCollection : Window
    {
        private ICollection<Product> products;
        private ListCollectionView view;

        public NavigateCollection()
        {
            InitializeComponent();

            products = App.StoreDb.GetProducts();

            this.DataContext = products;
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
            view.CurrentChanged += new EventHandler(view_CurrentChanged);

            lstProducts.ItemsSource = products;
        }

        private void cmdPrev_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToPrevious();
        }

        private void cmdNext_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToNext();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // view.MoveCurrentTo(lstProducts.SelectedItem);
        }

        private void view_CurrentChanged(object sender, EventArgs e)
        {
            lblPosition.Text = "Record" + (view.CurrentPosition + 1).ToString() + " of " + view.Count.ToString();
            cmdPrev.IsEnabled = view.CurrentPosition > 0;
            cmdNext.IsEnabled = view.CurrentPosition < view.Count - 1;
        }
    }
}
