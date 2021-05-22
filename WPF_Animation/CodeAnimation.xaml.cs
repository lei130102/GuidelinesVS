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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//使用XAML创建动画
//大多数情况下将使用XAML以声明方式创建动画，如故事板和事件触发器。然而使用XAML会涉及更多内容，因为需要另一个对象(故事板)将动画连接到
//恰当的属性。在某些情况下，当需要使用复杂逻辑为动画决定开始值和结束值时，基于代码的动画也是很有用的

//使用代码创建动画


//标准的帧频是60帧/秒，即WPF每隔1/60秒就会计算所有应用了动画的数值，并更新相应的属性

//使用动画的最简单方式是实例化在前面列出的其中一个动画类，配置该实例，然后使用希望修改的元素的BeginAnimation()方法。所有WPF元素，
//从UIElement基类开始，都继承了BeginAnimation()方法，该方法是IAnimatable接口的一部分。其他实现了IAnimatable接口的类包括
//ContentElement(文档流内容的基类)和Visual3D(3D可视化对象的基类)

namespace WPF_Animation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CodeAnimation : Window
    {
        public CodeAnimation()
        {
            InitializeComponent();
        }

        private void cmdGrow_Click(object sender, RoutedEventArgs e)
        {
            //使用线性插值的动画

            DoubleAnimation widthAnimation = new DoubleAnimation();
            //widthAnimation.From = 160;                                //开始值             From
            widthAnimation.To = this.Width - 30;                        //结束值             To
            widthAnimation.Duration = TimeSpan.FromSeconds(5);          //整个动画执行的时间  Duration
            widthAnimation.Completed += animation_Completed;

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.To = (this.Height - 50) / 3;
            heightAnimation.Duration = TimeSpan.FromSeconds(5);

            //同时发生的动画
            //可使用BeginAnimation()方法同时启动多个动画，BeginAnimation()方法几乎总是立即返回
            cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);
            cmdGrow.BeginAnimation(Button.HeightProperty, heightAnimation);
            //在本例中，两个动画没有被同步，这意味着宽度和高度不会准确地在相同时间间隔内增长(通常，将看到按钮先增加宽度，紧接着增加高度)
            //可通过创建绑定到同一个时间线的动画，突破这一限制

            //From属性

            //如果多次单击按钮，每次单击时，都会将Width属性重新设置为160，并且重新开始运行动画。即使当动画已在运行时单击按钮也同样如此
            //(可见每个依赖项属性每次只能响应一个动画，如果开始第二个动画，将自动放弃第一个动画)

            //在许多情况下，可能不希望动画从最初的From值开始。
            //1.创建能够被触发多次，并逐次累加效果的动画
            //例如，可能希望创建每次单击时都增大一点的按钮
            //2.创建可能相互重叠的动画
            //例如，可使用MouseEnter事件触发扩展按钮的动画，并使用MouseLeave事件触发将按钮缩小为原尺寸的互补动画。如果连续快速地将鼠标多次移动到这种按钮上
            //并移开，每个新动画就会打断上一个动画，导致按钮“跳”回到由From属性设置的尺寸
            //当前示例属于第二种情况。如果当按钮正在增大时单击按钮，按钮的宽度就会被重新设置为160像素——这可能会出现抖动效果
            
            //现在有一个问题，为使用这种技术，应用动画的属性必须有预先设置的值。这意味着按钮必须有硬编码的宽度(不管是在按钮标签中直接定义的，还是通过样式设置器应用的)。
            //问题是在许多布局容器中，通常不指定宽度并且让容器根据元素的对齐属性控制宽度。对于这种情况，元素使用默认宽度，也就是特殊的Double.NaN值(这里的NaN代表“
            //不是数字(not a number)”)。不能为具有这种值的属性使用线性插值应用动画

            //在许多情况下，解决方法是硬编码按钮的宽度，动画经常需要更精确地控制元素的尺寸和位置。实际上，对于能应用动画的内容，最常用的布局容器是Canvas面板，因为
            //Canvas面板允许更方便地移动内容(可能相互重叠)以及改变内容的尺寸。Canvas面板还是量级最轻的布局容器，因为当诸如Width的属性发生变化时不需要额外的布局工作

            //还有一种解决方法，可使用ActualWidth属性检索按钮的当前值，该属性给出的是按钮当前渲染的宽度。不能为ActualWidth属性应用动画(该属性是只读的)，但可以用该
            //属性设置动画的From属性
            //widthAnimation.From = cmdGrow.ActualWidth;
            //这种技术既可用于基于代码的动画(如当前示例)，也可用于将在后面介绍的声明式动画(这时需要使用绑定表达式来获取ActualWidth属性的值)
            //(注意Width属性反映的是选择的期望宽度，而ActualWidth属性指示的是最终使用的渲染宽度。如果使用自动布局，可能根本就没有设置硬编码的Width属性值，所以Width
            //属性只会返回Double.NaN值，并当试图开始动画时引发异常)
            //另一个问题是，当使用当前值作为动画的起点时——可能改变动画的运行速度。这是因为未调整动画的持续时间，使动画能够考虑到在初始值和最终值之间的跨度变小了
            //例如，假设创建的按钮不是使用From值而是从当前位置开始的动画。如果当几乎达到最大宽度值时单击按钮，新的动画就开始了。尽管只有几个像素的空间可供使用，但这个
            //动画仍被配置为持续5秒(通过Duration属性)。所以，按钮的增速看起来变慢了
            //只有当重新启动接近完成的动画时才会出现这样的效果。尽管有些古怪，但是大多数开发人员不会尝试为解决该问题而编写许多代码。相反，这被认为是可以接受的问题


            //To属性
            //也可以省略To属性

            //当省略From值时，动画使用当前值，并将动画纳入考虑范围

            //当忽略To值时，动画使用不考虑动画的当前值。本质上，这意味着To值变为原数值——最后一次在代码中、元素标签中或通过样式设置的值(这得益于WPF的属性识别系统，
            //该系统可以根据多个重叠属性提供者计算属性的值，不会丢弃任意信息)


            //Duration属性
            //动画开始时刻和结束时刻之间的时间间隔(时间间隔单位是毫秒、分钟、小时或您喜欢使用的其他任何单位)
            //动画的持续时间是使用TimeSpan对象设置的，但Duration属性实际上需要Duration对象。幸运的是，Duration和TimeSpan非常类似，并且Duration结构定义了一种
            //隐式转换，能够根据需要将System.TimeSpan转换为System.Windows.Duration
            //之所以要使用全新的数据类型，是因为Duration类型还提供了两个不能通过TimeSpan对象表示的特殊数值——Duration.Automatic和Duration.Forever
            //(Automatic值只将动画设置为1秒的持续时间，而Forever值使动画具有无限的持续时间，这会防止动画具有任何效果)
        }

        private void animation_Completed(object sender, EventArgs e)
        {
            //double currentWidth = cmdGrow.Width;
            //cmdGrow.BeginAnimation(Button.WidthProperty, null);
            //cmdGrow.Width = currentWidth;

            //MessageBox.Show("Completed!");
        }

        private void cmdShrink_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.Duration = TimeSpan.FromSeconds(5);
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.Duration = TimeSpan.FromSeconds(5);

            cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);
            cmdGrow.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        private void cmdGrowIncrementally_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.By = 10;                                                           //增加值      By
            widthAnimation.Duration = TimeSpan.FromSeconds(0.5);

            cmdGrowIncrementally.BeginAnimation(Button.WidthProperty, widthAnimation);

            //By属性
            //即使不使用To属性，也可以使用By属性。By属性用于创建按设置的数量改变值的动画，而不是按给定目标改变的值
            //这里
            //widthAnimation.By = 10;
            //等价于
            //widthAnimation.To = cmdGrowIncrementally.Width + 10;
            //然而当使用XAML定义动画时，使用By值就变得更加合理了，因为XAML没有提供执行简单计算的方法

            //注意，可结合使用By和From属性，但这并不会减少任何工作，By值被简单地增加到From值上，使其达到To值

            //大部分使用插值的动画类通常都提供了By属性，但并非全部如此。例如，对于非数值数据类型来说，By属性是没有意义的，比如ColorAnimation类使用的Color结构。

            //另有一种方法可得到类似的行为，而不需要使用By属性——可通过设置IsAdditive属性创建增加数值的动画。当创建这种动画时，当前值被自动添加到From值和To值。
            //例如：
            //DoubleAnimation widthAnimation = new DoubleAnimation();
            //widthAnimation.From = 0;
            //widthAnimation.To = -10;
            //widthAnimation.Duration = TimeSpan.FromSeconds(0.5);
            //widthAnimation.IsAdditive = true;
            //这个动画是从当前值开始的，当达到比当前少10个单位的值时完成。
            //另一方面，如果使用下面的动画：
            //DoubleAnimation widthAnimation = new DoubleAnimation();
            //widthAnimation.From = 10;
            //widthAnimation.To = 50;
            //widthAnimation.Duration = TimeSpan.FromSeconds(0.5);
            //widthAnimation.IsAdditive = true;
            //属性值跳到新值(比当前值大10个单位的值)，然后增加值，直到达到最后的值，最后的值比动画开始前的当前值大50个单位
        }
    }
}

//动画的声明周期

//WPF动画是暂时的，这意味着他们不能真正改变基本属性的值。当动画处于活动状态时，只是覆盖属性值。这是由依赖项属性的工作方式造成的，并且这是一个经常会被忽略的细节，
//该细节会给用户带来极大的困惑

//单向动画(如增长按钮的动画)在运行结束后会保持处于活动状态，这是因为动画需要将按钮的宽度保持为新值。这会导致如下不常见的问题——如果尝试使用代码在动画完成后修改属性值，
//代码将不起作用。因为代码只是为属性指定了一个新的本地值，但仍会优先使用动画之后的属性值

//可通过如下几种方法解决这个问题：
//1.创建将元素重新设置为原始状态的动画。可通过创建不设置To属性的动画达到此目的
//2.创建可翻转的动画。通过将AutoReverse属性设置为true来创建可翻转的动画
//3.改变FillBehavior属性。通常FillBehavior属性被设置为HoldEnd，这意味着当动画结束时，会继续为目标元素应用最后的值。如果将FillBehavior属性改为Stop，只要动画结束，属性就会恢复为原来的值
//4.当动画完成时通过处理动画对象的Completed事件删除动画对象
//首先，在启动动画前，关联事件处理程序以响应动画完成事件：
//widthAnimation.Completed += animation_Completed;
//注意，Completed事件是常规的.NET事件，使用常规的没有附加信息的EventArgs对象。该事件不是路由事件
//当引发Completed事件时，可通过调用BeginAnimation()方法来渲染不活动的动画。为此，只需要指定属性，并为动画对象传递null引用：
//cmdGrow.BeginAnimation(Button.WidthProperty, null);
//当调用BeginAnimation()方法时，属性返回为动画开始之前的原始值。如果这并非所希望的结果，可记下动画应用的当前值，删除动画，然后手动为属性设置新值
//double currentWidth = cmdGrow.Width;
//cmdGrow.BeginAnimation(Button.WidthProperty, null);
//cmdGrow.Width = currentWidth;
//(注意，现在改变了属性的本地值。这可能影响其他动画的运行。例如，如果为按钮使用未指定From属性的动画，该动画就会使用这个新应用的属性值作为起点。大多数情况下，
//这正是所希望的行为)


//(前3种方法改变了动画的行为。不管使用哪种方法，他们都将动画后的属性设置为原来的数值。如果这并非所希望的，那就需要使用最后一种方法)
