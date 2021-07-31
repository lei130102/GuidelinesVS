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
using System.Windows.Navigation;
using System.Windows.Shapes;


//鼠标光标

//可为任何元素使用Cursor属性以设置鼠标指针，该属性继承自FrameworkElement类

//可以通过System.Windows.Input.Cursor对象来表示每个光标。获取Cursor对象的最简易方法是使用Cursors类(位于System.Windows.Input命名空间)的静态属性，
//他们包含了所有标准的Windows鼠标光标，如沙漏光标、手状光标、调整尺寸的箭头光标等
//(Cursors类的属性获取在计算机上定义的鼠标光标。如果用户使用一套自定义的标准鼠标光标，那么所创建的应用程序将使用这些自定义的鼠标光标)

//如果使用XAML设置鼠标光标，就不需要直接使用Cursors类。这是因为Cursor属性的类型转换器能识别属性名称，并从Cursors类中检索对应的鼠标光标
//<Button Cursor="Help">Help</Button>

//有时可能设置相互重叠的光标。对于这种情况，会使用最特殊的光标。例如，可为一个按钮和包含该按钮的窗口设置不同的光标。当鼠标移到按钮上时，将显示为按钮设置的
//光标，而对于窗口中的其他区域则显示为窗口设置的光标。
//但有一个例外。通过使用ForceCursor属性，父元素可覆盖子元素的光标设置。将该属性设置为true时，会忽略子元素的Cursor属性，父元素的光标会被应用到内部的所有内容

//如果希望为应用程序每个窗口中的每个元素应用光标设置，使用FrameworkElement.Cursor属性将不起作用。相反，需要使用静态的Mouse.OverrideCursor属性，该属性覆盖
//每个元素的Cursor属性：
//Mouse.OverrideCursor = Cursors.Wait;
//为了移除应用程序范围的光标覆盖设置，需要将Mouse.OverrideCursor属性设为null



//WPF完全支持自定义光标。可使用普通的.cur光标文件(本质上是一幅小位图)，也可使用.ani动画光标文件
//要使用自定义的光标，需要为Cursor对象的构造函数传递光标文件的文件名或包含光标数据的流：

//Cursor customCursor = new Cursor(Path.Combine(applicationDir, "stopwatch.ani"));
//this.Cursor = customCursor;

//Cursor对象不直接支持URI资源语法，通过该语法，其他WPF元素(如Image对象)可使用保存在编译过的程序集中的文件。然而，可方便地为应用程序添加光标文件
//作为资源，然后作为可用于构造Cursor对象的数据流检索该资源。诀窍是使用Application.GetResourceStream()方法：

//StreamResourceInfo sri = Application.GetResourceStream(new Uri("stopwatch.ani", UriKind.Relative));
//Cursor customCursor = new Cursor(sri.Stream);
//this.Cursor = customCursor;
//(假定为项目添加了名为stopwatch.ani的文件，并将它的Build Action设置为Resource)


namespace WPF_Cursor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //将鼠标移到当前窗口上时，鼠标指针会变成沙漏图标
            this.Cursor = Cursors.Wait;
        }
    }
}
