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

namespace WPF_ComboBox
{
    public class Month
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    /// <summary>
    /// ItemsSource.xaml 的交互逻辑
    /// </summary>
    public partial class ItemsSource : Window
    {
        private List<Month> _monthes;
        public List<Month> Monthes
        {
            get
            {
                //ComboBox的ItemsSource一般绑定list
                _monthes = new List<Month>();
                _monthes.Add(new Month() { name = "一月", value = 1 });
                _monthes.Add(new Month() { name = "二月", value = 2 });
                _monthes.Add(new Month() { name = "三月", value = 3 });
                _monthes.Add(new Month() { name = "四月", value = 4 });
                _monthes.Add(new Month() { name = "五月", value = 5 });
                _monthes.Add(new Month() { name = "六月", value = 6 });
                _monthes.Add(new Month() { name = "七月", value = 7 });
                _monthes.Add(new Month() { name = "八月", value = 8 });
                _monthes.Add(new Month() { name = "九月", value = 9 });
                _monthes.Add(new Month() { name = "十月", value = 10 });
                _monthes.Add(new Month() { name = "十一月", value = 11 });
                _monthes.Add(new Month() { name = "十二月", value = 12 });

                return _monthes;
            }
        }
        public ItemsSource()
        {
            InitializeComponent();

            cbMonth.ItemsSource = Monthes;//(用于指定下拉列表绑定的List<string>数据对象)
            cbMonth.DisplayMemberPath = "name";//(下拉列表中选中行的索引)                                                  如果不设置DisplayMemberPath。ComboBox显示的是Month类型名(Month没有覆盖ToString()方法)。如果Month覆盖ToString()方法。将显示的是ToString()方法返回的字符串信息。
            cbMonth.SelectedValuePath = "value";//(下拉列表中要显示的List<T>数据对象的列，因为List<T>数据对象可能会有多列)    设置SelectedValuePath属性。如果Month覆盖了ToString()方法。SelectItem返回Month.ToString()返回的内容。SelectValue为Month中value的值
            cbMonth.SelectedIndex = 0;//(下拉列表中，对应与显示的List<T>数据对象的列，返回的List<T>数据对象的列)
        }

        private void cbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(cbMonth.SelectedValue.ToString());
        }

        private void result_Click(object sender, RoutedEventArgs e)
        {
            //空
        }
    }
}
