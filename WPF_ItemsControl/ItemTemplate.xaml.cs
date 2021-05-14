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


//當然ItemsControl並不是像前一個例子一樣逐一輸入, 而更像是其他WPF中的控件一樣做data binding, 這裡我們使用template來定義隱藏在類別的程式碼將會如何呈現。
//为了证明这一点，我举了一个例子，我们向用户显示了一个TODO列表，并向您展示了一旦​​定义自己的模板后所有内容的灵活性，我使用了ProgressBar控件来向您显示当前的完成情况百分比。首先是一些代码，然后是截图，然后是对它的解释：


//此示例中最重要的部分是我们在ItemsControl内部指定的模板，使用ItemsControl.ItemTemplate内部的DataTemplate标记。我们添加一个Grid面板，以获得两列：第一列我们有一个TextBlock，它将显示TODO项的标题，在第二列中我们有一个ProgressBar控件，我们绑定到Completion属性的值。

//模板现在代表一个TodoItem，我们在Code-behind文件中声明，我们还在其中实例化它们并将它们添加到列表中。最后，这个列表被分配给ItemsControl的ItemsSource属性，然后为我们完成剩下的工作。列表中的每个项目都使用我们的模板显示，您可以从生成的屏幕截图中看到。

namespace WPF_ItemsControl
{
    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }

    /// <summary>
    /// Binding.xaml 的交互逻辑
    /// </summary>
    public partial class ItemTemplate : Window
    {
        public ItemTemplate()
        {
            InitializeComponent();

            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

            icTodoList.ItemsSource = items;
        }
    }
}
