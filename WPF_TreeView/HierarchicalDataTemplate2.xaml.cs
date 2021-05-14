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
using System.Windows.Shapes;
using WPF_TreeView.HDT2;

namespace WPF_TreeView
{
    namespace HDT2
    {
        public class Family
        {
            public Family()
            {
                this.Members = new ObservableCollection<FamilyMember>();
            }

            public string Name { get; set; }

            public ObservableCollection<FamilyMember> Members { get; set; }  //注意是FamilyMember的集合不是Family的集合，不要求必须是Family的集合
        }

        public class FamilyMember
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }

    /// <summary>
    /// HierarchicalDataTemplate2.xaml 的交互逻辑
    /// </summary>
    public partial class HierarchicalDataTemplate2 : Window
    {
        public HierarchicalDataTemplate2()
        {
            InitializeComponent();

            List<Family> families = new List<Family>();

            Family family1 = new Family() { Name = "The Doe's" };
            family1.Members.Add(new FamilyMember() { Name = "John Doe", Age = 42 });
            family1.Members.Add(new FamilyMember() { Name = "Jane Doe", Age = 39 });
            family1.Members.Add(new FamilyMember() { Name = "Sammy Doe", Age = 13 });
            families.Add(family1);

            Family family2 = new Family() { Name = "The Moe's" };
            family2.Members.Add(new FamilyMember() { Name = "Mark Moe", Age = 31 });
            family2.Members.Add(new FamilyMember() { Name = "Norma Moe", Age = 28 });
            families.Add(family2);

            trvFamilies.ItemsSource = families;
        }
    }
}



//就像前面说到的，这两个模板被定义为TreeView的资源的一部分，并且允许TreeView根据不同的数据类型选择合适的模板进行展示。
//展示Family类型的模板被定义为分层模板(hierarchical template)，并且使用Members属性显示家庭成员。

//FamilyMember数据类型所使用的模板被定义为常规的数据模板(DataTemplate)，因为这个类型没有任何子成员。但是，如果我们希望所有FamilyMember
//都有子成员，并且这些子成员还有子成员，那么就应该使用分层模板(hierarchical template)

//在这两种模板中，我们都是用了一个图片表示了该节点是一个家庭还是家庭成员，并且展示了一些感兴趣的数据，比如家庭成员的数量，或者成员的年龄。
//在后台代码中，我们只是简单的创建了两个Family的实例，并且使用一组家庭成员填充他们，然后将这些Family加入到一个列表，也就是TreeView的数据源。
