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

//基于标记的绑定比通过代码创建的绑定更常见，但在一些特殊情况下，会希望使用代码创建绑定：
//1.创建动态绑定
//也可以在窗口的Resources集合中定义可能希望使用的每个绑定，并只添加使用合适的绑定对象调用SetBinding()方法的代码
//2.删除绑定
//需要借助BindingOperations.ClearBinding或者BindingOperations.ClearAllBindings方法。(不管绑定是通过代码应用的还是使用XAML标记应用的)
//仅为属性应用新值是不够的——如果正在使用双向绑定，设置的值会被传播到连接的对象，并且两个属性保持同步
//3.创建自定义控件
//为让他人能更容易地修改您构建的自定义控件的外观，需要将特定细节(如事件处理程序和数据绑定表达式)从标记移到代码中

namespace WPF_Binding
{
    /// <summary>
    /// ElementToElementBinding.xaml 的交互逻辑
    /// </summary>
    public partial class ElementToElementBinding : Window
    {
        public ElementToElementBinding()
        {
            InitializeComponent();
        }

        private void SetSmall_Click(object sender, RoutedEventArgs e)
        {
            sliderFontSize.Value = 2;
        }

        private void SetNormal_Click(object sender, RoutedEventArgs e)
        {
            sliderFontSize.Value = this.FontSize;
        }

        private void SetLarge_Click(object sender, RoutedEventArgs e)
        {
            //Only works in two-way mode.
            lblSampleText.FontSize = 30;
        }

        //使用代码创建绑定

        //Binding binding = new Binding();
        //binding.Source = sliderFontSize;
        //binding.Path = new PropertyPath("Value");
        //binding.Mode = BindingMode.TwoWay;
        //lblSampleText.SetBinding(TextBlock.FontSize, binding);




        //使用代码移除绑定

        //BindingOperations.ClearBinding(lblSampleText, TextBlock.FontSize);
        //BindingOperations.ClearAllBindings(lblSampleText);

        //BindingOperations.ClearBinding和BindingOperations.ClearAllBindings都调用DependencyObject.ClearValue()方法

        private void GetBoundObject_Click(object sender, RoutedEventArgs e)
        {
            //使用代码检索绑定     (不必考虑绑定最初是用代码还是标记创建的)
            //Binding binding = BindingOperations.GetBinding(txtBound, TextBox.TextProperty);

            //Binding.Path           提供的PropertyPath对象从绑定对象提取绑定值
            //Binding.Path.Path      获取绑定属性的名称(这里是Value)
            //Binding.Mode           用于告知绑定何时更新目标元素

            //BindingExpression expression = BindingOperations.GetBindingExpression(txtBound, TextBox.TextProperty);

            //BindingExpression对象包括一些属性，用于复制Binding对象提供的信息
            //BindingExpression.ResolvedSource        该属性允许计算绑定表达式并获得其结果——传递的本地数据

            //BindingExpression对象仅是将两项内容封装到一起的较小组装包，这两项内容是：
            //1.Binding对象(通过BindingExpression.ParentBinding属性提供)
            //2.由源绑定的对象(通过BindingExpression.DataItem属性提供)

            Binding binding = BindingOperations.GetBinding(txtBound, TextBox.TextProperty);
            MessageBox.Show("Bound " + binding.Path.Path + " to source element " + binding.ElementName);

            BindingExpression expression = BindingOperations.GetBindingExpression(txtBound, TextBox.TextProperty);
            MessageBox.Show("Bound " + expression.ResolvedSourcePropertyName + " with data " + ((TextBlock)expression.ResolvedSource).FontSize);
        }
    }
}
