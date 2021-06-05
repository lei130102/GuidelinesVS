using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//特别注意，因为生成的.xaml通过添加x:Class生成的GradientButton.g.cs中的命名空间要跟这里一直
namespace WPF_ControlTemplate.Resources
{
    /// <summary>
    /// 需要手工创建资源字典.xaml对应的.xaml.cs
    /// 注意.xaml文件中的ResourceDictionary标签中要有x:Class="WPF_ControlTemplate.Resources.GradientButton"
    /// </summary>
    public partial class GradientButton : System.Windows.ResourceDictionary
    {
        /// <summary>
        /// 需要手工创建构造函数和调用InitializeComponent函数
        /// </summary>
        public GradientButton()
        {
            InitializeComponent();
        }
    }
}
