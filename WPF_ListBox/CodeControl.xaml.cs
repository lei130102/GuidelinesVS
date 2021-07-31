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
    public class Item
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }

    /// <summary>
    /// Control.xaml 的交互逻辑
    /// </summary>
    public partial class CodeControl : Window
    {
        public CodeControl()
        {
            InitializeComponent();

            List<Item> items = new List<Item>();
            items.Add(new Item() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new Item() { Title = "Learn C#", Completion = 80 });
            items.Add(new Item() { Title = "Wash the car", Completion = 0 });

            lbTodoList.ItemsSource = items;
        }

        private void btnShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            //SelectedItems
            foreach (object obj in lbTodoList.SelectedItems)
            {
                MessageBox.Show((obj as Item).Title);
            }
        }

        private void btnSelectNext_Click(object sender, RoutedEventArgs e)
        {
            //SelectedIndex
            int nextIndex = 0;
            if((lbTodoList.SelectedIndex >= 0) && (lbTodoList.SelectedIndex < (lbTodoList.Items.Count - 1)))
            {
                nextIndex = lbTodoList.SelectedIndex + 1;
            }
            lbTodoList.SelectedIndex = nextIndex;
        }

        private void btnSelectCSharp_Click(object sender, RoutedEventArgs e)
        {
            //Items
            foreach(object obj in lbTodoList.Items)
            {
                if((obj is Item) && ((obj as Item).Title.Contains("#")))
                {
                    lbTodoList.SelectedItem = obj;
                    break;
                }
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(object obj in lbTodoList.Items)
            {
                lbTodoList.SelectedItems.Add(obj);
            }
        }

        private void lbTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SelectedItem
            if(lbTodoList.SelectedItem != null)
            {
                this.Title = (lbTodoList.SelectedItem as Item).Title;
            }
        }
    }
}
