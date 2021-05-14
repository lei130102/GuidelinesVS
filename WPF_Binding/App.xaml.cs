using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//数据绑定

//数据绑定是一种关系，该关系告诉WPF从源对象提取一些信息，并用这些信息设置目标对象的属性。目标属性始终是依赖项属性，通常位于WPF元素中——毕竟，
//WPF数据绑定的最终目标是在用户界面中显示一些信息。然而，源对象可以是任何内容

//1.绑定到元素对象
//需要使用ElementName
//当源对象的属性也是依赖项属性时，因为依赖项属性具有内置的更改通知支持，所以不必为此构建任何额外的基础结构

//2.绑定到非元素对象
//需要使用:
//a.Source
//该属性是指向源对象的引用——换句话说，是提供数据的对象
//b.RelativeSource
//可在当前元素(包含绑定表达式的元素)的基础上构建引用。这似乎无谓地增加了复杂程度，但实际上，RelativeSource属性是一种特殊工具，当编写控件模板以及数据模板时是很方便的
//c.DataContext
//如果没有使用Source或者RelativeSource属性指定源，WPF就从当前元素开始在元素树种向上查找。检查每个元素的DataContext属性，并使用第一个非null的
//DataContext属性，当我要将同一个对象的多个属性绑定到不同的元素时，DataContext属性是非常有用的，因为可在更高层次的容器对象上(而不是直接在目标元素上)设置DataContext属性
//源对象的属性必须是公有属性，不能是私有属性或者公有字段

namespace WPF_Binding
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
