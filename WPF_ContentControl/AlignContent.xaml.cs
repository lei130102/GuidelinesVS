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

//对齐内容

//使用在FrameworkElement基类中定义的HorizontalAlignment和VerticalAlignment属性，在容器中对齐不同的控件
//使用在Control基类中定义的HorizontalContentAlignment和VerticalContentAlignment属性，在内容控件中的内容如何和边框对齐

//可将内容对齐到控件的任意边缘(Top Bottom Left Right)，可以居中(Center)，也可以拉伸内容使其充满可用空间(Stretch)

//通过Margin属性可在相邻元素之间添加空间
//内容控件使用Padding属性在控件边缘和内容边缘之间添加空间

//HorizontalContentAlignment、VerticalContentAlignment以及Padding属性都是在Control类中定义的，而并非是在更特殊的ContentControl
//类中定义的，这是因为可能有些控件不是内容控件，但也需要包含某些类型的内容，比如TextBox控件——通过使用对齐方式和内边距设置来调整他所包含的文本(存储在Text属性中)


namespace WPF_ContentControl
{
    /// <summary>
    /// AlignContent.xaml 的交互逻辑
    /// </summary>
    public partial class AlignContent : Window
    {
        public AlignContent()
        {
            InitializeComponent();
        }
    }
}
