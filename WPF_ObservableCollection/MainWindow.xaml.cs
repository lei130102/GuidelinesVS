using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


//
//msdn中 ObservableCollection<T> 类 表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知。
//
//在许多情况下，所使用的数据是对象的集合。 例如，数据绑定中的一个常见方案是使用 ItemsControl（如 ListBox、ListView 或 TreeView）来显示记录的集合。
//
//可以枚举实现 IEnumerable 接口的任何集合。 但是，若要设置动态绑定，以便集合中的插入或删除操作可以自动更新 UI，则该集合必须实现 INotifyCollectionChanged 接口。 此接口公开 CollectionChanged 事件，只要基础集合发生更改，都应该引发该事件。
//
//WPF 提供 ObservableCollection<T> 类，它是实现 INotifyCollectionChanged 接口的数据集合的内置实现。
//
//还有许多情况，我们所使用的数据只是单纯的字段或者属性，此时我们需要为这些字段或属性实现INotifyPropertyChanged接口，实现了该接口，只要字段或属性的发生了改变，就会提供通知机制。


namespace WPF_ObservableCollection
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Students> infos = new ObservableCollection<Students>()
        {
            new Students(){ Id=1, Age=11, Name="Tom"},
            new Students(){ Id=2, Age=12, Name="Drren"},
            new Students(){ Id=3, Age=13, Name="Jacky"},
            new Students(){ Id=4, Age=14, Name="Andy"}
        };

        public MainWindow()
        {
            InitializeComponent();

            this.lbStudent.ItemsSource = infos;
            this.txtStudentId.SetBinding(TextBox.TextProperty, new Binding("SelectedItem.Id") { Source = lbStudent });
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            infos[1] = new Students() { Id = 4, Age = 14, Name = "这是一个集合改变" };
            infos[2].Name = "这是一个属性改变";
        }
    }
}

//在这个例子中我们将Students数据对象用ObservableCollection<T>来修饰。这样当我们点击click的时候我们看到。当我们点击后只有student整个对象的改变引发了后台通知机制。
