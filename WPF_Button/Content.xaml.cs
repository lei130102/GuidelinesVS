﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

//按钮

//WPF提供了三种类型的按钮控件：(都是继承自ButtonBase类的内容控件)
//a.Button GridViewColumnHeader RepeatButton ToggleButton
//b.CheckBox
//c.RadioButton

//ButtonBase类增加了几个成员：

//Click事件
//通过鼠标或者键盘触发

//ClickMode属性
//该属性决定何时引发Click事件以响应鼠标动作。
//ClickMode.Release(默认值)       当单击和释放鼠标键时引发Click事件
//ClickMode.Press                 当鼠标第一次按下时引发Click事件
//ClickMode.Hover                 当鼠标移动到按钮上并在按钮上悬停一会儿就会引发Click事件

//(所有按钮控件都支持访问键，访问键和Label控件中的助记符类似。添加下划线字符来标识访问键。如果用户按下了Alt键和访问键，就会触发按钮单击事件)


//GridViewColumnHeader
//当使用基于网格的ListView控件时，该类表示一列可以单击的标题

//RepeatButton
//只要按钮保持按下状态，该类就不断地触发Click事件。对于普通按钮，用户每次单击只触发一个Click事件

//ToggleButton
//该类表示具有两个状态(按下状态和未按下状态)的按钮。当单击ToggleButton按钮时，他会保持按下状态，直到再次单击该按钮以释放他为止。这有时称为
//“粘贴单击”行为

//RepeatButton和ToggleButton类都是在System.Windows.Controls.Primitives命名空间中定义的，这表明他们通常不单独使用。相反，他们通常通过组合来构建
//更复杂的控件，或通过继承扩展其功能。
//RepeatButton类和ToggleButton类都不是抽象类，所以可以在用户界面中直接使用他们。ToggleButton控件在工具栏中非常有用

//(RepeatButton类常用于构建高级的ScrollBar控件(最终，甚至ScrollBar控件都是更高级的ScrollViewer控件的一部分)。RepeatButton类使滚动条两端的箭头按钮具有
//他们所特有的行为——只要按住箭头按钮不释放就会一直滚动。类似地，ToggleButton控件通常也用于派生出更有用的CheckBox类和RadioButton类)

namespace WPF_Button
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Content : Window
    {
        public Content()
        {
            InitializeComponent();
        }

        private void helloworld_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello, world!");
        }
    }
}
