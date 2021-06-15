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
    /// BindProductObject.xaml 的交互逻辑
    /// </summary>
    public partial class BindProductObject : Window
    {
        public BindProductObject()
        {
            InitializeComponent();
        }

        //首次运行这个应用程序时，不会显示信息。虽然定义了绑定，但不能获得源对象

        //当用户在运行过程中单击按钮时，使用StoreDB类获取合适的产品数据。尽管可通过编写代码创建每个绑定，但这不尽合理(并且相对于手工填充控件，不会
        //节省大量代码)。然而，DataContext属性提供了一种完美的快捷方式。如果为包含所有数据绑定表达式的Grid控件设置该属性，所有绑定表达式都会通过
        //该属性使用数据填充自身





        //具有null值的绑定

        //当前的Product类假定他将获得产品数据的所有内容。然而，数据表经常包含可空字段，这里的空值表示缺失的或不能使用的信息。在数据类中，可通过为
        //简单值类型(如数字和日期)使用可空数据类型反映这个情况。例如在Product类中，可用decimal?代替decimal。当然，引用类型，如字符串和完整对象，
        //总是支持null值

        //绑定null值的结果是可预测的：目标元素根本不显示任何内容。对于数字字段，这一行为是有用的，因为他能够区分缺少数值(对于这种情况，元素不显示
        //任何内容)和零值(对于这种情况，元素显示文本"0")的情况。然而，值得注意的是，可通过在绑定表达式中设置TargetNullValue属性来改变WPF对null值
        //的处理方式。如果设置了该属性，当数据源具有null值时，将显示您提供的值

        //比如，当Production.Description属性为null时，该例显示文本"[No Description Provided]"：
        //Text = "{Binding Path=Description, TargetNullValue=[No Description Provided]}"
        //TargetNullValue文本周围的方括号是可选的。在这里，他们的目的是帮助用户确定显示的文本并非来自数据库

        private void cmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if(Int32.TryParse(txtID.Text, out ID))
            {
                try
                {
                    gridProductDetails.DataContext = App.StoreDb.GetProduct(ID);
                }
                catch
                {
                    MessageBox.Show("Error contacting database.");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }
        }
    }
}
