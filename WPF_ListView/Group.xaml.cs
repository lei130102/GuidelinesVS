using System;
using System.Collections.Generic;
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
using WPF_ListView.G;

//ListView分组
//正如我们之前已经讨论的那样，WPF ListView非常灵活。分组是它开箱即用的另一个东西，它既易于使用又极易定制

//在XAML中，我向ListView添加了一个GroupStyle，在其中我为每个组的标题定义了一个模板。它由一个TextBlock控件组成，我在其中使用了一个略大
//且粗体的文本来显示它是一个组 - 正如我们稍后将看到的，这当然可以定制得更多。 TextBlock Text属性绑定到Name属性，但请注意，这不是数据对
//象上的Name属性（在本例中为User类）。相反，它是由WPF分配的组的名称，基于我们用于将对象分成组的属性。

//在分配ItemsSource之后，我们使用它来获取ListView为我们创建的CollectionView。此专用View实例包含许多可能性，包括对项目进行分组的功能。
//我们通过向视图的GroupDescriptions添加所谓的PropertyGroupDescription来使用它。这基本上告诉WPF按数据对象的特定属性进行分组，在本例中为Sex属性。


namespace WPF_ListView
{
    namespace G
    {
        public enum SexType { Male, Female};

        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Mail { get; set; }
            public SexType Sex { get; set; }
        }

        //绘制一个三角形，无论是向上还是向下，取决于排序方向。 WPF使用adorners的概念允许你在其他控件上绘制东西，这正是我们想要的：在ListView列标题之上绘制排序三角形的能力。

        //SortAdorner通过定义两个Geometry对象来工作，这些对象基本上用于描述2D形状 - 在这种情况下是一个三角形，尖端朝上，另一个尖端朝下。 Geometry.Parse()方法使用点列表绘制三角形，这将在后面的文章中更全面地解释。

        //SortAdorner知道排序方向，因为它需要绘制正确的三角形，但不知道我们排序的字段 - 这是在UI层中处理的。

        //User类只是一个基本信息类，用于包含有关用户的信息。其中一些信息在UI层中使用，我们将其绑定到Name，Age和Sex属性。

        //在Window类中，我们有两个方法：构造函数，我们构建用户列表并将其分配给ListView的ItemsSource，然后是用户单击列时将触及的更有趣的单击事件处理程序。在类的顶部，我们定义了两个私有变量：listViewSortCol和listViewSortAdorner。这些将有助于我们跟踪我们当前正在排序的列和我们放置的装饰器以指示它。

        //在lvUsersColumnHeader_Click事件处理程序中，我们首先获取用户单击的列的引用。有了这个，我们可以简单地通过查看我们在XAML中定义的Tag属性来决定要对User类中的哪个属性进行排序。然后我们检查我们是否已按列排序 - 如果是这种情况，我们会删除装饰器并清除当前的排序说明。

        //在那之后，我们准备好决定方向。默认值是升序，但我们会检查是否已经按用户单击的列进行排序 - 如果是这种情况，我们会将方向更改为降序。

        //最后，我们创建一个新的SortAdorner，传入应该渲染的列以及方向。我们将它添加到列标题的AdornerLayer中，最后，我们向ListView添加一个SortDescription，让它知道要排序的属性和方向。

        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
            private static Geometry descGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if(AdornedElement.RenderSize.Width < 20)
                {
                    return;
                }

                TranslateTransform transform = new TranslateTransform
                    (
                        AdornedElement.RenderSize.Width - 15,
                        (AdornedElement.RenderSize.Height - 5) / 2
                    );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                {
                    geometry = descGeometry;
                }
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }
    }

    /// <summary>
    /// Group.xaml 的交互逻辑
    /// </summary>
    public partial class Group : Window
    {
        private GridViewColumnHeader listViewSortCol4 = null;
        private SortAdorner listViewSortAdorner4 = null;

        public Group()
        {
            InitializeComponent();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
            items.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
            items.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });

            lvUsers.ItemsSource = items;

            PropertyGroupDescription propertyGroupDescription = new PropertyGroupDescription("Sex");
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            view.GroupDescriptions.Add(propertyGroupDescription);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            List<User> items2 = new List<User>();
            items2.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
            items2.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
            items2.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });

            lvUsers2.ItemsSource = items2;

            PropertyGroupDescription propertyGroupDescription2 = new PropertyGroupDescription("Sex");
            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers2.ItemsSource);
            view2.GroupDescriptions.Add(propertyGroupDescription2);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            List<User> items3 = new List<User>();
            items3.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
            items3.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
            items3.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });

            lvUsers3.ItemsSource = items3;

            CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers3.ItemsSource);
            view3.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            view3.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            //视图将先使用年龄进行排序，当年龄相同时，使用名称排序

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            List<User> items4 = new List<User>();
            items4.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
            items4.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
            items4.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });

            lvUsers4.ItemsSource = items4;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            List<User> items5 = new List<User>();
            items5.Add(new User() { Name = "John Doe", Age = 42 });
            items5.Add(new User() { Name = "Jane Doe", Age = 39 });
            items5.Add(new User() { Name = "Sammy Doe", Age = 13 });
            items5.Add(new User() { Name = "Donna Doe", Age = 13 });
            lvUsers5.ItemsSource = items5;

            //获取对ListView的CollectionView实例的引用，然后将委托分配给Filter属性。这个委托指向名为UserFilter的函数，我们在下面实现了它。它将每个项目作为第一个（也是唯一的）参数，然后返回一个布尔值，指示给定项目是否应该在列表中可见。
            CollectionView view5 = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers5.ItemsSource);
            view5.Filter = UserFilter;
        }

        /// <summary>
        /// 在UserFilter()方法中，我们看看TextBox控件（txtFilter），看看它是否包含任何文本 - 如果是，我们用它来检查User的名称（这是我们的属性）
        /// 已经决定过滤）包含输入的字符串，然后根据它返回true或false。如果TextBox为空，则返回true，因为在这种情况下，我们希望所有项都可见。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool UserFilter(object item)
        {
            if(String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }
            else
            {
                return ((item as User).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if(listViewSortCol4 != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol4).Remove(listViewSortAdorner4);
                lvUsers4.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if(listViewSortCol4 == column && listViewSortAdorner4.Direction == newDir)
            {
                newDir = ListSortDirection.Descending;
            }

            listViewSortCol4 = column;
            listViewSortAdorner4 = new SortAdorner(listViewSortCol4, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol4).Add(listViewSortAdorner4);
            lvUsers4.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        /// <summary>
        /// txtFilter_TextChanged事件也很重要。每次文本更改时，我们都会获得对ListView的View对象
        /// 的引用，然后在其上调用Refresh()方法。这可确保每次用户更改搜索/过滤字符串文本框的值时都会调用过滤器委托。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers5.ItemsSource).Refresh();
        }
    }
}
