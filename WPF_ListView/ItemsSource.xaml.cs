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
using WPF_ListView.IS;

namespace WPF_ListView
{
    namespace IS
    {
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }

            //通过重写ToString()方法来获取实际有意义的输出结果。让我们把User类进行以下的修改
            public override string ToString()
            {
                return this.Name + ", " + this.Age + " years old";
            }

            //现在ListView里展示的结果就能让人看懂了，这种方法在某些场景下也能获得不错的结果。但我们并不总想展示单纯的string文本。也许我们希望这个文本的某一部分可以加粗或者用另一种颜色展示，又或者我们想展示一张图片。幸运的是，通过模板(templates)，我们在WPF中可以非常简单的实现这一点。
        }
    }
    /// <summary>
    /// ItemsSource.xaml 的交互逻辑
    /// </summary>
    public partial class ItemsSource : Window
    {
        public ItemsSource()
        {
            InitializeComponent();

            //给这个list添加了一些自定义的User实例，每个User实例包含一个姓名(name)和对应的年龄(age)属性。数据绑定会在我们把这个list赋给ListView的ItemsSource属性时自动执行，但这时的结果却有点出乎我们的意料:
            //在ListView中每个User显示的是这个类的类型名称。这实际上是预料之内的，因为.NET并不知道该如何展示我们的数据，所以它就调用每个object的ToString()方法并用这个结果来表示每项。
            List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42 });
            items.Add(new User() { Name = "Jane Doe", Age = 39 });
            items.Add(new User() { Name = "Sammy Doe", Age = 13 });
            lvDataBinding.ItemsSource = items;
        }
    }
}
