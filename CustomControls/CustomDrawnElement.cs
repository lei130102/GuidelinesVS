using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


//自定义绘图元素

//前面已经开始分析WPF元素的内部工作原理——允许每个元素插入到WPF布局系统的MeasureOverride()和ArrangeOverride()方法中。本节将进一步
//深入分析和研究元素如何渲染他们自身

//大多数WPF元素通过组合方式创建可视化外观。换句话说，典型的元素通过其他更基础的元素进行构建。例如，使用标记定义用户控件的组合元素，处理
//标记的方式与自定义窗口中的XAML相同。使用控件模板为自定义控件定义可视化树。并且当创建自定义面板时，根本不必定义任何可视化细节。组合元素
//由控件使用者提供，并添加到Children集合中

//当然，直到现在才能使用组合。最终，一些类需要负责绘制内容。在WPF中，这些类位于元素树的底层。在典型窗口中，是通过单独的文本、形状以及位图
//执行渲染的，而不是通过高级元素





//OnRender()方法

//为了执行自定义渲染，元素必须重写OnRender()方法，该方法继承自UIElement基类。OnRender()方法未必不需要替换组合——一些控件使用
//OnRender()方法绘制可视化细节并使用组合在其上叠加其他元素。Border和Panel类是两个例子，Border类在OnRender()方法中绘制边框，
//Panel类在OnRender()方法中绘制背景。Border和Panel类都支持子内容，并且这些子内容在自定义的绘图细节之上进行渲染

//OnRender()方法接收一个DrawingContext对象，该对象为绘制内容提供了一套很有用的方法。第一次学习DrawingContext类的相关内容是在
//第14章，在该章中使用该类为Visual对象绘制内容。在OnRender()方法中执行绘图的主要区别是不能显式地创建和关闭DrawingContext对象。
//这是因为几个不同的OnRender()方法可能使用相同的DrawingContext对象。例如，派生的元素可以执行一些自定义绘图操作并调用基类中的
//OnRender()方法来绘制其他内容。这种方法是可行的，因为当开始这一过程时，WPF会自动创建DrawingContext对象，并且当不再需要时关闭该对象

//注意：从技术角度看，OnRender()方法实际上没有将内容绘制到屏幕上，而是绘制到DrawingContext对象上，然后WPF缓存这些信息。WPF决定
//元素何时需要重新绘制并绘制使用DrawingContext对象创建的内容。这是WPF保留模式图形系统的本质——由您定义内容，WPF无缝地管理绘制和
//刷新过程

//关于WPF渲染，最令人惊奇的细节是实际上只需要使用很少的类。大多数类是通过其他更简单的类构建的，并且对于典型的控件，为了找到实际重写
//OnRender()方法的类，需要进入到控件元素树中非常深的层次。下面是一些重写了OnRender()方法的类：
//1.TextBlock类
//无论在何处放置文本，都有TextBlock对象使用OnRender()方法绘制文本
//2.Image类
//Image类重写OnRender()方法，使用DrawingContext.DrawImage()方法绘制图形内容
//3.MediaElement类
//如果正在使用该类播放视频文件，该类会重写OnRender()方法以绘制视频帧
//4.各种形状类
//Shape基类重写了OnRender()方法，通过使用DrawingContext.DrawGeometry()方法，绘制在其内部存储的Geometry对象。根据Shape类的特定派生类，Geometry
//对象可以表示椭圆、矩形或更复杂的由直线和曲线构成的路径。许多元素使用形状绘制小的可视化细节
//5.各种修饰类
//这些类(如ButtonChrome和ListBoxChrome)绘制通用控件的外侧外观，并在具体制定的内部放置内容。其他许多继承自Decorator的类，如Border类，都重写了
//OnRender()方法
//6.各种面板类
//尽管面板的内容是由其子元素提供的，但是OnRender()方法绘制具有背景色(假设设置了Background属性)的矩形

//通常，OnRender()方法的实现看起来很简单。例如，下面是继承自Shape类的所有渲染代码：
//protected override void OnRender(DrawingContext drawingContext)
//{
//  this.EnsureRenderedGeometry();
//  if(this._renderedGeometry != Geometry.Empty)
//  {
//      drawingContext.DrawGeometry(this.Fill, this.GetPen(), this._renderedGeometry);
//  }
//}
//请记住，重写OnRender()方法不是渲染内容并且将其添加到用户界面的唯一方法。也可以创建DrawingVisual对象，并使用AddVisualChild()方法为UIElement对象
//添加该可视化对象(并实现其他一些细节)，然后可以调用DrawingVisual.RenderOpen()方法为DrawingVisual对象检索DrawingContext对象，并使用返回的
//DrawingContext对象渲染DrawingVisual对象的内容

//在WPF中，一些元素使用这种策略在其他元素内容之上显示一些图形细节。例如，在拖放指示器、错误指示器以及焦点框中可以看到这种情况。在所有这些情况中，
//DrawingVisual类允许元素在其他内容之上绘制内容，而不是在其他内容之下绘制内容，但对于大部分情况，是在专门的OnRender()方法中进行渲染



//评估自定义绘图

//当创建自定义元素时，可能会选择重写OnRender()方法来绘制自定义内容。可在包含内容的元素(最常见的情况是继承自Decorator的类)中重写OnRender()方法，
//从而可以在内容周围添加图形装饰。也可以在没有任何嵌套内容的元素中重写OnRender()方法，从而可以绘制元素的整个可视化外观。例如，可以创建绘制一些小的
//图形细节的自定义元素，然后可以通过组合，在其他类中使用自定义元素。WPF中的这方面示例是TickBar元素，该元素为Slider控件绘制刻度标记。TickBar元素通过
//Slider控件的默认控件模板(该模板还包括一个Border和一个Track元素，Track元素又包含了两个RepeatButton控件和一个Thumb元素)嵌入到Slider控件的可视化树中

//一个明显的问题是需要确定何时使用较低级的OnRender()方法，以及何时使用其他类(例如，继承自Shape类的元素)的组合来绘制所需的内容。为了做出决定，需要评估所需
//图形的复杂程度以及希望提供的交互能力

//例如，分析一下ButtonChrome类。在ButtonChrome类的WPF实现中，自定义的渲染代码考虑了各种属性，包括RenderDefaulted、RenderMouseOver以及RenderPressed。
//Button类的默认控件模板在适当的时机使用触发器设置这些属性，例如，当将鼠标移动到按钮上时，Button类使用触发器将ButtonChrome.RenderMouseOver属性设置为true

//无论何时改变RenderDefaulted、RenderMouseOver或RenderPressed属性，ButtonChrome类都会调用基本的InvalidateVisual()方法来指示当前外观不再有效。WPF然后
//调用ButtonChrome.OnRender()方法来获取新的图形表示

//如果ButtonChrome类使用组合，这种行为就更难实现。使用合适的元素为ButtonChrome类创建标准外观很容易，但是当按钮的状态发生变化时，需要做更多的工作来修改外观。
//需要动态改变构成ButtonChrome类的嵌套元素，如果外观变化很大的话，就必须隐藏一个元素并在合适的位置显示另一个元素

//大多数自定义元素不需要自定义渲染。但是当属性发生变化或执行特定操作时，需要渲染复杂的变化很大的可视化外观，此时使用自定义的渲染方法可能更加简单并且更便捷








//自定义绘图元素

//现在您已经知道了OnRender()方法的工作原理，以及何时使用该方法。最后一步是分析一个演示使用OnRender()方法的自定义控件

//自定义绘图元素CustomDrawnElement使用RadialGradientBrush画刷绘制阴影背景。技巧是动态设置强调显示的渐变起点，使其跟随鼠标。从而当用户在
//控件上移动鼠标时，白色的发光中心点跟随鼠标移动

//CustomDrawnElement元素不需要包含任何子内容，所以他直接继承自FrameworkElement类。该元素只提供了一个可以设置的属性——渐变的背景色(前景色被硬编码为白色，尽管
//可以很容易地改变这一细节)

namespace CustomControls
{
    public class CustomDrawnElement : FrameworkElement
    {
        public static DependencyProperty BackgroundColorProperty;

        static CustomDrawnElement()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(Colors.Yellow);
            metadata.AffectsRender = true;
            BackgroundColorProperty = DependencyProperty.Register("BackgroundColor",
                typeof(Color), typeof(CustomDrawnElement), metadata);
        }


        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        //BackgroundColor依赖项属性使用FrameworkPropertyMetadata.AffectRender标志明确进行了标识。因此，无论何时改变了背景色，WPF都自动调用
        //OnRender()方法。然而，当鼠标移动到新的位置时，也需要确保调用OnRender()方法。这是通过在合适的时间调用InvalidateVisual()方法实现的

        //名为GetForegroundBrush()的私有辅助方法根据鼠标的当前位置构造正确的RadialGradientBrush画刷。为了计算中心点，需要将鼠标在元素上悬停的当前
        //位置转换成从0到1的相对位置，这正是RadialGradientBrush画刷期望的结果
        private Brush GetForegroundBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }
            else
            {
                RadialGradientBrush brush = new RadialGradientBrush(Colors.White, BackgroundColor);
                Point absoluteGradientOrigin = Mouse.GetPosition(this);
                Point relativeGradientOrigin = new Point(
                    absoluteGradientOrigin.X / base.ActualWidth, absoluteGradientOrigin.Y / base.ActualHeight);

                brush.GradientOrigin = relativeGradientOrigin;
                brush.Center = relativeGradientOrigin;
                brush.Freeze();
                return brush;
            }
        }

        //渲染代码使用DrawingContext.DrawRectangle()方法绘制元素的背景
        //ActualWidth和ActualHeight属性指示控件最终的渲染尺寸
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            Rect bounds = new Rect(0, 0, base.ActualWidth, base.ActualHeight);
            dc.DrawRectangle(GetForegroundBrush(), null, bounds);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.InvalidateVisual();
        }

    }
}
