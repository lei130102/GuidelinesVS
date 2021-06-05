using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

//创建自定义面板

//到目前为止，您已经看到了如何从头开发两个自定义控件：自定义的ColorPicker和FlipPanel控件
//接下来的几节将考虑两个更特殊的选择：派生自定义面板以及构建自定义绘图控件

//创建自定义面板是一种特殊但较常见的自定义控件开发子集，面板驻留一个或多个子元素，并且实现了特定的布局逻辑以恰当地安排其子元素。
//如果希望构建自己的可拖动的工具栏或可停靠的窗口系统，自定义面板是很重要的元素。当创建需要非标准特定布局的组合控件时，自定义面板
//通常是很有用的，例如花哨的停靠工具栏











//两步布局过程

//每个面板都使用相同的设备：负责改变子元素尺寸和安排子元素的两步布局过程。第一个阶段是测量阶段(measure pass)，在这一阶段面板决定
//其子元素希望具有多大的尺寸。第二个阶段是排列阶段(layout pass)，在这一阶段为每个控件指定边界。这两个步骤是必需的，因为在决定如何
//分割可用空间时，面板需要考虑所有子元素的期望

//可以通过重写名称奇特的MeasureOverride()和ArrangeOverride()方法，为这两个步骤添加自己的逻辑，这两个方法是作为WPF布局系统的一部分
//在FrameworkElement类中定义的。奇特的名称使用表示MeasureOverride()和ArrangeOverride()方法代替在MeasureCore()和ArrangeCore()
//方法中定义的逻辑，后两个方法是在UIElement类中定义的。这两个方法是不能被重写的

//1.MeasureOverride()方法

//第一步是首先使用MeasureOverride()方法决定每个子元素希望多大的空间。然而，即使是在MeasureOverride()方法中，也不能为子元素提供无限空间。至少，也应当
//将子元素限制在能够适应面板可用空间的范围之内。此外，可能希望更严格地限制子元素。例如，具有按比例分配尺寸的两行的Grid面板，会为子元素提供可用高度的一半。
//StackPanel面板会为第一个元素提供所有可用空间，然后为第二个元素提供剩余的空间，等等。

//每个MeasureOverride()方法的实现负责遍历子元素集合，并调用每个子元素的Measure()方法。当调用Measure()方法时，需要提供边界框——决定每个子控件最大可用
//空间的Size对象。在MeasureOverride()方法的最后，面板返回显示所有子元素所需的空间，并返回他们所期望的尺寸

//下面是MeasureOverride()方法的基本结构，其中没有具体的尺寸细节
//protected override Size MeasureOverride(Size constraint)
//{
//  //Examine all the children.
//  foreach(UIElement element in base.InternalChildren)
//  {
//      // Ask each child how much space it would like, given the availableSize constraint.
//      Size availableSize = new Size(...);
//      element.Measure(availableSize);
//      // (You can now read element.DesiredSize to get the requested size.)
//  }
//  // Indicate how much space this panel requires.
//  // This will be used to set the DesiredSize property of the panel.
//  return new Size(...);
//}
//Measure()方法不返回数值。在为每个子元素调用Measure()方法之后，子元素的DesiredSize属性提供了请求的尺寸。可以在为后续子元素执行计算时
//(以及决定面板需要的总空间时)使用这一信息

//因为许多元素直到调用了Measured()方法之后才会渲染他们自身，所以必须为每个子元素调用Measure()方法，即使不希望限制子元素的尺寸或使用DesiredSize
//属性也同样如此。如果希望让所有子元素能够自由获得他们所希望的全部空间，可以传递在两个方向上的值都是Double.PositiveInfinity的Size对象
//(ScrollViewer是使用这种策略的一个元素，原因是他可以处理任意数量的内容)。然后子元素会返回其中所有内容所需要的空间。否则，子元素通常会返回
//其中内容需要的空间或者可用空间——返回较小者

//在测量过程的结尾，布局容器必须返回他所期望的尺寸。在简单的面板中，可以通过组合每个子元素的期望尺寸计算面板所希望的尺寸

//注意：不能为面板的期望尺寸简单地返回传递给MeasureOverride()方法的限制范围。尽管这看起来是获取所有可用空间的好方法，但如果容器传递Size对象，
//而且Size对象的一个方向或两个方向上的数值是Double.PositiveInfinity(这意味着"占用需要的所有控件空间")，这时就会出现麻烦。尽管对于尺寸限制范围来说，
//无限的尺寸是允许的，但是对于尺寸结果，无限的尺寸是不允许的，因为WPF不能计算出元素应当多大。另外，实际上不应当使用超出需要的更大空间。如果这样做的话，
//可能会导致额外的空白空间，并且布局面板之后的元素会在窗口中进一步下移

//细心的读者可能已经注意到为每个子元素调用的Measure()方法和定义面板布局逻辑第一步的MeasureOverride()方法极其相似。实际上，Measure()方法会触发
//MeasureOverride()方法。所以，如果在一个布局容器中放置另一个布局容器，当调用Measure()方法时，将会得到布局容器及其所有子元素所需的总尺寸

//提示：通过两步执行测量过程(触发MeasureOverride()方法的Measure()方法)的一个原因是为了处理外边距。当调用Measure()方法时，传递总的可用空间。当WPF
//调用MeasureOverride()方法时，考虑到外边距空间，会自动减少可用空间(除非传递无限的尺寸)

//2.ArrangeOverride()方法

//测量完所有元素后，就可以在可用的空间中排列元素了。布局系统调用面板的ArrangeOverride()方法，而面板为每个子元素调用Arrange()方法，以告诉子元素为他分配了
//多大的空间(您可能已经猜到了，Arrange()方法会触发ArrangeOverride()方法，这与Measure()方法会触发MeasureOverride()方法非常类似)

//当使用Measure()方法测量条目时，传递能够定义可用空间边界的Size对象。当使用Arrange()方法放置条目时，传递能够定义条目尺寸和位置的System.Windows.Rect对象。
//这时，就像使用Canvas面板风格的X和Y坐标放置每个元素一样(坐标确定布局容器左上角与元素左上角之间的距离)

//注意：元素(以及布局面板)可以随意打破这些规则，并试图超出为他们分配的边界。例如，之前看到了Line元素是如何重叠在相邻元素之上的。然而，普通元素应当遵循为
//其提供的边界。此外，大多数容器会剪裁超出边界的子元素

//下面是ArrangeOverride()方法的基本结构，其中没有给出具体的尺寸细节：
//protected override Size ArrangeOverride(Size arrangeSize)
//{
//  // Examine all the children
//  foreach(UIElement element in base.InternalChildren)
//  {
//      // Assign the child it's bounds.
//      Rect bounds = new Rect(...);
//      element.Arrange(bounds);
//      // (You can now read element.ActualHeight and element.ActualWidth to find out the size it used...)
//  }
//
//  // Indicate how much space this panel occupies.
//  // This will be used to set the ActualHeight and ActualWidth properties of the panel.
//  return arrangeSize;
//}

//当排列元素时，不能传递无限尺寸。然而，可以通过传递来自DesiredSize属性的值，为元素提供他所期望的值。也可以为元素提供比所需尺寸更大的空间。实际上，
//经常会出现这种情况。例如，垂直的StackPanel面板为其子元素提供所请求的高度，但是为子元素提供面板本身的整个宽度。同样，Grid面板使用具有固定尺寸或
//按比例计算尺寸的行，这些行的尺寸可能大于其内部元素所期望的尺寸。即使已经在根据内容改变尺寸的容器中放置了元素，如果使用Height和Width属性明确设置了
//元素的尺寸，那么仍可以扩展该元素

//当使元素比所期望的尺寸更大时，就需要使用HorizontalAlignment和VerticalAlignment属性。元素内容被放在指定边界内部的某个位置

//因为ArrangeOverride()方法总是接收定义的尺寸(而非无限的尺寸)，所以为了设置面板的最终尺寸，可以返回传递的Size对象。实际上，许多布局容器就是采用这一
//步骤来占据提供的所有空间(不能冒险占用其他控件可能需要的空间，因为除非有可用空间，否则布局系统的测量步骤一定不会为元素提供超出需要的空间)





//Canvas面板的副本

//理解这两个方法的最快捷方式是研究Canvas类的内部工作原理，Canvas是最简单的布局容器。为了创建自己的Canvas风格的面板，只需要简单地继承Panel类，并且添加
//MeasureOverride()和ArrangeOverride()方法，如下所示：

//public class CanvasClone : System.Windows.Controls.Panel
//{...}

//Canvas面板在他们希望的位置放置子元素，并且为子元素设置他们希望的尺寸。所以，Canvas面板不需要计算如何分割可用空间。这使得MeasureOverride()方法非常简单。
//为每个子元素提供无限的空间：

//protected override Size MeasureOverride(Size constraint)
//{
//  Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
//  foreach(UIElement element in base.InternalChildren)
//  {
//      element.Measure(size);
//  }
//  return new Size();
//}
//注意，MeasureOverride()方法返回空的Size对象。这意味着Canvas面板根本不请求任何空间，而是由您明确地为Canvas面板指定尺寸，或者将其放置到布局容器中进行
//拉伸以填充整个容器的可用空间

//ArrangeOverride()方法包含的内容稍微多一些。为了确定每个元素的正确位置，Canvas面板使用附加属性(Left、Right、Top以及Bottom)。正如之前学习过的，附加属性
//是使用定义类中的两个辅助方法实现的：GetProperty()和SetProperty()方法

//在此分析的Canvas面板副本更简单一些——只使用Left和Top附加属性(而不考虑多余的Right和Bottom属性)。下面是用于排列元素的代码：
//protected override Size ArrangeOverride(Size arrangeSize)
//{
//    foreach(UIElement element in base.InternalChildren)
//    {
//        double x = 0;
//        double y = 0;
//        double left = Canvas.GetLeft(element);
//        if(!DoubleUtil.IsNaN(left))
//        {
//            x = left;
//        }
//        double top = Canvas.GetTop(element);
//        if(!DoubleUtil.IsNaN(top))
//        {
//            y = top;
//        }
//        element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
//    }
//    return arrangeSize;
//}






//自定义WrapPanel面板

//WrapPanel面板执行一个简单的功能，该功能有时十分有用。该面板逐个地布置其子元素，一旦当前行的宽度用完，就会切换到下一行。但有时您需要采用一种方法来
//强制立即换行，以便在新行中启动某个特定控件。尽管WrapPanel面板原本没有提供这一功能，但通过创建自定义控件可以方便地添加该功能。只需要添加一个请求换行的
//附加属性即可。此后，面板中的子元素可使用该属性在适当位置换行


namespace CustomControls
{
    public class WrapBreakPanel : Panel
    {
        //自定义附加属性。当将该属性设置为true时，这个属性会导致在元素之前立即换行

        //与所有依赖项属性一样，LineBreakBefore属性被定义成静态字段，然后在自定义类的静态构造函数中注册该属性。唯一的区别在于进行注册时使用的是
        //RegisterAttached()方法而非Register()方法

        //用于LineBreakBefore属性的FrameworkPropertyMetadata对象明确指定该属性影响布局过程。所以，无论何时设置该属性，都会触发新的排列阶段

        //这里没有使用常规属性封装器封装这些附加属性，因为不在定义他们的同一个类中设置他们，相反，需要提供两个静态方法，这两个方法能够使用
        //DependencyObject.SetValue()方法在任意元素上设置这个属性
        public static DependencyProperty LineBreakBeforeProperty;

        static WrapBreakPanel()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.AffectsArrange = true;
            metadata.AffectsMeasure = true;
            LineBreakBeforeProperty = DependencyProperty.RegisterAttached("LineBreakBefore", typeof(bool), typeof(WrapBreakPanel), metadata);

        }
        public static void SetLineBreakBefore(UIElement element, Boolean value)
        {
            element.SetValue(LineBreakBeforeProperty, value);
        }
        public static Boolean GetLineBreakBefore(UIElement element)
        {
            return (bool)element.GetValue(LineBreakBeforeProperty);
        }

        //唯一保留的细节是当执行布局逻辑时需要考虑该属性。WrapBreakPanel面板的布局逻辑以WrapPanel面板的布局逻辑为基础。在测量阶段，元素按行排列，从而
        //使面板能够计算需要的总空间。除非太大或者LineBreakBefore属性被设置为true，否则每个元素都被添加到当前行中

        protected override Size MeasureOverride(Size constraint)
        {
            Size currentLineSize = new Size();
            Size panelSize = new Size();

            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(constraint);
                Size desiredSize = element.DesiredSize;

                if (GetLineBreakBefore(element) ||
                    currentLineSize.Width + desiredSize.Width > constraint.Width)
                {
                    // Switch to a new line (either because the element has requested it
                    // or space has run out).
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    panelSize.Height += currentLineSize.Height;
                    currentLineSize = desiredSize;

                    // If the element is too wide to fit using the maximum width of the line,
                    // just give it a separate line.
                    if (desiredSize.Width > constraint.Width)
                    {
                        panelSize.Width = Math.Max(desiredSize.Width, panelSize.Width);
                        panelSize.Height += desiredSize.Height;
                        currentLineSize = new Size();
                    }
                }
                else
                {
                    // Keep adding to the current line.
                    currentLineSize.Width += desiredSize.Width;

                    // Make sure the line is as tall as its tallest element.
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                }
            }

            // Return the size required to fit all elements.
            // Ordinarily, this is the width of the constraint, and the height
            // is based on the size of the elements.
            // However, if an element is wider than the width given to the panel,
            // the desired width will be the width of that line.
            panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
            panelSize.Height += currentLineSize.Height;
            return panelSize;
        }

        //上述代码中的重要细节是检查LineBreakBefore属性，这实现了普通WrapPanel面板没有提供的额外逻辑






        //ArrangeOverride()方法的代码几乎相同，但更枯燥一些。区别在于：面板在开始布局一行之前需要决定该行的最大高度(根据最高的元素确定)。这样，
        //每个元素可以得到完整数量的可用空间，可用空间占用行的整个高度。与使用普通WrapPanel面板进行布局时的过程相同

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            int firstInLine = 0;

            Size currentLineSize = new Size();

            double accumulatedHeight = 0;

            UIElementCollection elements = base.InternalChildren;
            for (int i = 0; i < elements.Count; i++)
            {

                Size desiredSize = elements[i].DesiredSize;

                if (GetLineBreakBefore(elements[i]) || currentLineSize.Width + desiredSize.Width > arrangeBounds.Width) //need to switch to another line
                {
                    arrangeLine(accumulatedHeight, currentLineSize.Height, firstInLine, i);

                    accumulatedHeight += currentLineSize.Height;
                    currentLineSize = desiredSize;

                    if (desiredSize.Width > arrangeBounds.Width) //the element is wider then the constraint - give it a separate line                    
                    {
                        arrangeLine(accumulatedHeight, desiredSize.Height, i, ++i);
                        accumulatedHeight += desiredSize.Height;
                        currentLineSize = new Size();
                    }
                    firstInLine = i;
                }
                else //continue to accumulate a line
                {
                    currentLineSize.Width += desiredSize.Width;
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                }
            }

            if (firstInLine < elements.Count)
                arrangeLine(accumulatedHeight, currentLineSize.Height, firstInLine, elements.Count);

            return arrangeBounds;
        }

        private void arrangeLine(double y, double lineHeight, int start, int end)
        {
            double x = 0;
            UIElementCollection children = InternalChildren;
            for (int i = start; i < end; i++)
            {
                UIElement child = children[i];
                child.Arrange(new Rect(x, y, child.DesiredSize.Width, lineHeight));
                x += child.DesiredSize.Width;
            }
        }

    }


}
