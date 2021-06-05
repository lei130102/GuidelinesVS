using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

//支持可视化状态

//ColorPicker控件是控件设计的极佳示例，因为其行为和可视化外观是精心分离的，所以其他设计人员可开发动态改变其外观的新模板

//ColorPicker控件如此简单的一个原因是他不涉及状态。换句话说，他不根据是否具有焦点、鼠标是否在他上面悬停、是否禁用等状态区分其可视化外观。接下来的
//FlipPanel自定义控件有些不同

//FlipPanel控件背后的基本思想是，为驻留内容提供两个表面，但每次只有一个表面是可见的。为看到其他内容，需要在两个表面之间进行“翻转”。可通过
//控件模板定制翻转效果，但默认效果使用在前面和后面之间进行过渡的淡化效果。根据应用程序，可以使用FlipPanel控件把数据条目表单与一些有帮助的
//文档组合起来，以便为相同的数据提供一个简单或较复杂的视图，或在一个简单游戏中将问题和答案融合在一起

//可通过代码执行翻转(通过设置名为IsFlipped的属性)，也可使用一个便捷的按钮来翻转面板(除非控件使用者从模板中移除了该按钮)

//显然，控件模板需要指定两个独立部分：FlipPanel控件的前后内容区域。然而，还有一个细节——FlipPanel控件需要一种方法在两个状态之间进行切换：
//翻转过的状态与未翻转过的状态。可通过为模板添加触发器来完成该工作。当单击按钮时，可使用一个触发器隐藏前面的面板并显示第二个面板，而使用
//另一个触发器翻转这些更改。这两个触发器都可以使用您喜欢的任何动画。但通过使用可视化状态，可向控件使用者清晰地指名这两个状态是模板的必需
//部分。不是为适当的属性或事件编写触发器，控件使用者只需要填充适当的状态动画。如果使用Expression Blend，该任务甚至变得更简单

namespace CustomControls
{
    //FlipPanel的基本骨架非常简单。包含用户可用单一元素(最有可能是包含各种元素的布局容器)填充的两个内容区域。从技术角度看，这意味着
    //FlipPanel控件不是真正的面板，因为不能使用布局逻辑组织一组子元素。然而，这不会造成问题，因为FlipPanel控件的结构是清晰直观的。
    //FlipPanel控件还包含一个翻转按钮，用户可使用该按钮在两个不同的内容区域之间进行切换



    //尽管可通过继承自ContentControl或Panel等控件类来创建自定义控件，但是FlipPanel直接继承自Control基类。如果不需要特定控件类的功能，
    //这是最好的起点。
    //不应当继承自更简单的FrameworkElement类，除非希望创建不使用标准控件和模板基础架构的元素

    //为表明FlipPanel使用这些部件和状态的事实，应为自定义控件类应用TemplatePart特性
    //FlipButton和FlipButtonAlternate部件都受到限制——每个部件只能是ToggleButton控件或ToggleButton派生类的实例
    //(ToggleButton是可单击的按钮，能够处于两个状态中的某个状态。对于FlipPanel控件，ToggleButton的状态对应于普通的前向视图或翻转的后向视图)

    //提示：为确保最好、最灵活的模板支持，尽可能使用最通用的元素类型。例如，除非需要ContentControl提供的某些属性或行为，使用
    //FrameworkElement比使用ContentControl更好
    [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton)),
    TemplatePart(Name = "FlipButtonAlternate", Type = typeof(ToggleButton)),
    TemplateVisualState(Name = "Normal", GroupName = "ViewStates"),
    TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
    public class FlipPanel : Control
    {
        //定义并初始化依赖项属性

        //FrontContent依赖项属性，类型为object，保存在前表面上显示的元素
        public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);

        //BackContent依赖项属性，类型为object，保存在后表面上显示的元素
        public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);

        //IsFlipped依赖项属性，类型为bool，持续跟踪FlipPanel控件的当前状态(面向前面还是面向后面)，使控件使用者能够通过编程翻转状态
        public static readonly DependencyProperty IsFlippedProperty = DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), null);

        //FlipPanel类不需要更多的依赖项属性，因为他实际上从Control类继承了他所需要的几乎所有内容。一个例外是CornerRadius属性
        //尽管Control类包含了BorderBrush和BorderThickness属性，可以使用这些属性在FlipPanel控件上绘制边框，但他缺少将方形边缘变成光滑曲线的CornerRadius属性，
        //如Border元素所做的那样。在FlipPanel控件中实现类似的效果很容易，前提是添加CornerRadius依赖项属性并使用该属性配置FlipPanel控件的默认控件模板中的Border元素
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);

        public object FrontContent
        {
            get
            {
                return GetValue(FrontContentProperty);
            }
            set
            {
                SetValue(FrontContentProperty, value);
            }
        }

        public object BackContent
        {
            get
            {
                return GetValue(BackContentProperty);
            }
            set
            {
                SetValue(BackContentProperty, value);
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        public bool IsFlipped
        {
            get
            {
                return (bool)GetValue(IsFlippedProperty);
            }
            set
            {
                SetValue(IsFlippedProperty, value);

                //调用自定义方法，确保更新显示以匹配当前的翻转状态(面向前面还是面向后面)
                ChangeVisualState(true);
            }
        }

        static FlipPanel()
        {
            //为通过控件从generic.xaml文件获取默认样式，需要在FlipPanel类的静态构造函数中调用DefaultStyleKeyProperty.OverrideMetadata方法
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel), new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        //关联元素

        //在FlipPanel.xaml中定义好默认控件模板后，需要在FlipPanel控件中添加一些内容以使该模板工作
        //诀窍是使用OnApplyTemplate()方法，该方法还用于在ColorPicker控件中设置绑定。对于FlipPanel控件，OnApplyTemplate()方法用于为
        //FlipButton和FlipButtonAlternate部件检索ToggleButton，并为每个部件关联事件处理程序，从而当用户单击以翻转控件时能够进行响应。
        //最后，OnApplyTemplate()方法调用名为ChangeVisualState()的自定义方法，该方法确保控件的可视化外观和其当前状态相匹配：

        //提示：当调用GetTemplateChild()方法时，需要给出希望获取的元素的字符串名称。为避免可能的错误，可在控件中将该字符串声明为常量。然后在
        //TemplatePart特性中以及调用GetTemplateChild()方法时可以使用该常量

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ToggleButton flipButton = base.GetTemplateChild("FlipButton") as ToggleButton;
            if (flipButton != null) flipButton.Click += flipButton_Click;

            // Allow for two flip buttons if needed (one for each side of the panel).
            // This is an optional design, as the control consumer may use template
            // that places the flip button outside of the panel sides, like the 
            // default template does.
            ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
            if (flipButtonAlternate != null)
                flipButtonAlternate.Click += flipButton_Click;

            this.ChangeVisualState(false);
        }

        //下面是非常简单地允许用户点击ToggleButton按钮并翻转面板的事件处理程序


        private void flipButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsFlipped = !this.IsFlipped;
        }

        //幸运的是，不需要手动触发状态动画。既不需要创建也不需要触发过渡动画。相反，为从一个状态改变到另一个状态，只需要调用静态方法VisualStateManager.GoToState()。
        //当调用该方法时，传递正在改变状态的控件对象的引用、新状态的名称以及确定是否显示过渡的Boolean值。如果是由用户引发的改变(例如，当用户单击ToggleButton按钮时)，
        //该值应当为true；如果是由属性设置引发的改变(例如，如果使用页面的标记设置IsFlipped属性的初始值)，该值为false

        //处理控件支持的所有不同状态可能会变得很凌乱。为避免在整个控件代码中分散调用GoToState()方法，大多数控件添加了与在FlipPanel控件中添加的ChangeVisualState()类似
        //的方法。该方法负责应用每个状态组中的正确状态。该方法中的代码使用if语句块(或switch语句)应用每个状态组的当前状态。该方法之所以可行，是因为他完全可以使用当前状态
        //的名称调用GoToState()方法。在这种情况下，如果当前状态和请求的状态相同，那么什么也不会发生

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.IsFlipped)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }

            // Disable flipped side to prevent tabbing to invisible buttons.            
            UIElement front = FrontContent as UIElement;
            if (front != null)
            {
                if (IsFlipped)
                {
                    front.Visibility = Visibility.Hidden;
                }
                else
                {
                    front.Visibility = Visibility.Visible;
                }
            }
            UIElement back = BackContent as UIElement;
            if (back != null)
            {
                if (IsFlipped)
                {
                    back.Visibility = Visibility.Visible;
                }
                else
                {
                    back.Visibility = Visibility.Hidden;
                }
            }
        }

        //通常在以下位置调用ChangeVisualState()方法或与其等效的方法：
        //1.在OnApplyTemplate()方法的结尾，在初始化控件之后
        //2.当响应代表状态变化的事件时，例如鼠标移动或单击ToggleButton按钮
        //3.当响应属性改变或通过代码触发方法时(例如，IsFlipped属性设置器调用ChangeVisualState()方法并且总是提供true，所以显示过渡动画。如果
        //希望为控件使用者提供不显示过渡的机会，可添加Flip()方法，该方法接受与为ChangeVisualState()方法传递的相同的Boolean参数)

        //正如上面介绍的，FlipPanel控件非常灵活。例如，可使用该控件并且不使用ToggleButton按钮，通过代码进行翻转(可能是当用户单击不同的控件时)。也可在
        //控件模板中包含一两个翻转按钮，并且允许用户进行控制

    }
}
