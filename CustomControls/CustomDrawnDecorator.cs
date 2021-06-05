using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

//创建自定义装饰元素

//作为一条通用规则，切勿在控件中使用自定义绘图。如果在控件中使用自定义绘图，就违反了WPF无外观控件的承诺。问题是一旦硬编码一些绘图逻辑，就会使
//控件可视化外观的一部分不能通过控件模板进行定制

//更好的方法是设计单独的绘制自定义内容的元素(例如CustomDrawnElement类)，然后在控件的默认模板内部使用自定义元素。很多WPF控件使用这种方法

//有必要快速分析一下如何修改上一个示例，使其能够成为控件模板的一部分。在控件模板中，自定义绘图元素通常扮演两个角色：
//1.他们绘制一些小的图形细节(例如滚动按钮上的箭头)
//2.他们在另一个元素的周围提供更加详细的背景或边框

//第二种方法需要自定义装饰元素。可以通过两个轻微的改动将CustomDrawnElement类转换成自定义绘图元素
//首先，使CustomDrawnDecorator类继承自Decorator类
//然后，重写OnMeasure()方法，指定需要的尺寸。所有装饰元素都会考虑他们的子元素，增加装饰所需要的额外空间，然后返回组合之后的尺寸。

namespace CustomControls
{
    public class CustomDrawnDecorator : Decorator
    {
        static CustomDrawnDecorator()
        {
            CustomDrawnElement.BackgroundColorProperty.AddOwner(
                typeof(CustomDrawnDecorator));
        }


        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(CustomDrawnElement.BackgroundColorProperty);
            }
            set
            {
                SetValue(CustomDrawnElement.BackgroundColorProperty, value);
            }
        }

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
                brush.RadiusX = 0.2;
                brush.Center = relativeGradientOrigin;
                brush.Freeze();
                return brush;
            }
        }
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

        //CustomDrawnDecorator类不需要任何额外的控件来绘制边框，相反，使用下面的代码简单地使自身和其内容具有相同的尺寸
        protected override Size MeasureOverride(Size constraint)
        {
            UIElement child = this.Child;
            if (child != null)
            {
                child.Measure(constraint);
                return child.DesiredSize;
            }
            else
            {
                return new Size();
            }

        }
    }
}

//一旦创建自定义装饰元素，就可以在自定义控件模板中使用他们。例如，下面的按钮模板在按钮内容的后面放置了跟随鼠标踪迹的渐变背景
//使用模板绑定确保使用对齐属性和内边距属性
//<ControlTemplate x:Key="ButtonWithCustomChrome">
//  <lib:CustomDrawnDecorator BackgroundColor="LightGreen">
//      <ContentPresenter Margin="{TemplateBinding Padding}" HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
//      VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
//      ContentTemplate="{TemplateBinding ContentControl.ContentTemplate}"
//      Content="{TemplateBinding ContentControl.Content}"
//      RecognizesAccessKey="True"/>
//  </lib:CustomDrawnDecorator>
//</ControlTemplate>
//现在可以使用这个模板重新样式化按钮，使其具有新的外观。当然，为了使自定义装饰元素更加实用，当单击鼠标按钮时可能希望改变他的外观。使用
//修改装饰类属性的触发器可以完成该工作
