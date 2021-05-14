using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WPF_TreeView.HDT3;

namespace WPF_TreeView
{
    namespace HDT3
    {
        public class TreeViewItemBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public void RaisePropertyChanged(string propertyName)
            {
                if(this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            private bool _isSelected;
            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    if(_isSelected != value)
                    {
                        _isSelected = value;
                        RaisePropertyChanged("IsSelected");
                    }
                }
            }

            private bool _isExpanded;
            public bool IsExpanded
            {
                get
                {
                    return _isExpanded;
                }
                set
                {
                    if(_isExpanded != value)
                    {
                        this._isExpanded = value;
                        RaisePropertyChanged("IsExpanded");
                    }
                }
            }
        }

        public class Person : TreeViewItemBase
        {
            public Person()
            {
                this.Children = new ObservableCollection<Person>();
            }

            public string Name { get; set; }
            public int Age { get; set; }

            public ObservableCollection<Person> Children { get; set; }
        }
    }
    /// <summary>
    /// HierarchicalDataTemplate3.xaml 的交互逻辑
    /// </summary>
    public partial class HierarchicalDataTemplate3 : Window
    {
        public HierarchicalDataTemplate3()
        {
            InitializeComponent();

            List<Person> persons = new List<Person>();
            Person person1 = new Person() { Name = "John Doe", Age = 42 };
            Person person2 = new Person() { Name = "Jane Doe", Age = 39 };
            Person child1 = new Person() { Name = "Sammy Doe", Age = 13 };
            person1.Children.Add(child1);
            person2.Children.Add(child1);
            person2.Children.Add(new Person() { Name = "Jenny Moe", Age = 17 });
            Person person3 = new Person() { Name = "Becky Toe", Age = 25 };
            persons.Add(person1);
            persons.Add(person2);
            persons.Add(person3);

            person2.IsExpanded = true;
            person2.IsSelected = true;

            trvPersons.ItemsSource = persons;
        }

        private void btnSelectNext_Click(object sender, RoutedEventArgs e)
        {
            if(trvPersons.SelectedItem != null)
            {
                var list = (trvPersons.ItemsSource as List<Person>);
                int curIndex = list.IndexOf(trvPersons.SelectedItem as Person);
                if(curIndex >= 0)
                {
                    curIndex++;
                }
                if(curIndex >= list.Count)
                {
                    curIndex = 0;
                }
                if(curIndex >= 0)
                {
                    list[curIndex].IsSelected = true;
                }
            }
        }

        private void btnToggleExpansion_Click(object sender, RoutedEventArgs e)
        {
            if(trvPersons.SelectedItem != null)
            {
                (trvPersons.SelectedItem as Person).IsExpanded = !(trvPersons.SelectedItem as Person).IsExpanded;
            }
        }
    }
}


//XAML 部分

//我已经定义了几个按钮放在对话框的底部，以使用这两个新属性。然后我们有了TreeView，我已经为它定义了一个ItemTemplate
//（如前一章所示）以及一个ItemContainerStyle。如果你还没有阅读关于样式的章节，你可能不完全理解那个部分，但它只是将我
//们自己的自定义类上的属性与TreeViewItems上的IsSelected和IsExpanded属性结合在一起，这是使用Style完成的。 setter方法。
//您可以在本教程的其他地方了解更多相关信息。

//后置代码部分
//在后置代码中，我定义了一个Person类，它有几个属性，它们从TreeViewItemBase类继承了我们的额外属性。您应该知道TreeViewItemBase类
//实现了INotifyPropertyChanged接口，并使用它来通知这两个基本属性的更改 - 如果没有这个，选择/扩展更改将不会反映在UI中。通知更改的
//概念在数据绑定章节中进行了解释。

//在主窗口类中，我只创建一系列人员，同时为其中一些人添加子项。我将这些人添加到一个列表中，我将其指定为TreeView的ItemsSource，它在定义
//的模板的帮助下，以截图显示的方式呈现它们。

//当我在person2对象上设置IsExpanded和IsSelected属性时，会发生最有趣的部分。这是导致第二人（Jane Doe）最初被选中和扩展的原因，如屏幕截
//图所示。我们还在两个测试按钮的事件处理程序中对Person对象（继承自TreeViewItemBase类）使用这两个属性（请记住，为了使代码尽可能小而简单，
//选择按钮仅适用于顶级项目）。
