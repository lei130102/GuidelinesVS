using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

//创建无外观控件

//用户控件的目标是提供增补控件模板的设计表面，提供一种定义控件的快速方法，代价是失去了将来的灵活性。如果喜欢用户控件的功能，但需要修改其
//可视化外观，使用这种方法就有问题了。例如，设想希望使用相同的颜色拾取器，但希望使用不同的“皮肤”，将其更好地融合到已有的应用程序窗口中。
//可以通过样式来改变用户控件的某些方面，但该控件的一些部分是在内部锁定的，并硬编码到标记中，例如，无法将预览矩形移动到滑动条的左边

//解决方法是创建无外观控件——继承自控件基类，但没有设计表面的控件。相反这个控件将其标记放到默认模板中，可替换默认模板而不会影响控件逻辑

namespace CustomControls
{
    //ColorPicker类继承自System.Windows.Controls.Control。继承自FrameworkElement类是不合适的，因为颜色拾取器允许与用户进行交互，
    //而且其他高级的类不能准确地描述颜色拾取器的行为，例如颜色拾取器不允许在内部嵌套其他内容，所以继承自ContentControl类也是不合适的
    [TemplatePart(Name = "PART_RedSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_BlueSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_GreenSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_PreviewBrush", Type = typeof(SolidColorBrush))]
    public class ColorPicker : System.Windows.Controls.Control
    {
        //ColorPicker类中的代码与用于用户控件的代码是相同的(除了必须删除构造函数中的InitializeComponent()方法调用)。可使用相同的
        //方法定义依赖项属性和路由事件。唯一的区别是需要通知WPF，将为控件类提供新样式。该样式将提供新的控件模板(如果不执行该步骤，将
        //继续使用在基类中定义的模板)

        static ColorPicker()
        {
            //为通知WPF正在提供新的样式，需要在自定义控件类的静态构造函数中调用OverrideMetadata()方法。需要在DefaultStyleKeyProperty属性
            //上调用该方法，该属性是为自定义控件定义默认样式的依赖项属性

            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));

            //如果希望使用其他控件类的模板，可提供不同的类型，但几乎总是为每个自定义控件创建特定的样式


            //修改颜色拾取器的标记

            //添加对OverrideMetadata()方法的调用后，只需要插入正确的样式。需要将样式放在名为generic.xaml的资源字典中，该资源字典
            //必须放在项目文件夹的Themes子文件夹中。这样，该样式就会被识别为自定义控件的默认样式





            //主题专用的样式和generic.xaml文件

            //您已经看到，ColorPicker从generic.xaml文件获取默认的控件模板，generic.xaml文件位于Themes项目文件夹中。这个稍有些怪异的约定
            //实际上是旧式WPF功能的一部分：Windows主题支持
            //Windows主题支持的初衷是使开发人员创建控件的自定义版本来匹配不同的Windows主题。Windows XP计算机使用主题来控制Windows应用程序
            //的总体颜色方案，Windows主题支持在此类计算机上最有意义。Windows Vista引入了Aero主题，该主题有效地取代了旧的主题选项。后续Windows
            //版本尚未改变这种事态，因此人们现在普遍忽略了原来就不怎么常用的WPF中的Windows主题功能
            //不过，当今的WPF应用程序开发人员总是使用generic.xaml文件来设置默认的控件样式。generic.xaml文件的名称(及其所在的Themes文件夹)
            //被延用下来





            ColorProperty = DependencyProperty.Register("Color", typeof(Color),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorChanged)));

            RedProperty = DependencyProperty.Register("Red", typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            GreenProperty = DependencyProperty.Register("Green", typeof(byte),
                typeof(ColorPicker),
                    new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            BlueProperty = DependencyProperty.Register("Blue", typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
        }

        public ColorPicker()
        {
            //InitializeComponent();
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            // Set up command bindings.
            CommandBinding binding = new CommandBinding(ApplicationCommands.Undo,
             UndoCommand_Executed, UndoCommand_CanExecute);

            this.CommandBindings.Add(binding);
        }

        private Color? previousColor;
        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = previousColor.HasValue;
        }
        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Use simple reverse-or-redo Undo (like Notepad).
            this.Color = (Color)previousColor;
        }


        public static DependencyProperty ColorProperty;
        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;

            Color oldColor = (Color)e.OldValue;
            Color newColor = (Color)e.NewValue;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;

            colorPicker.previousColor = oldColor;
            colorPicker.OnColorChanged(oldColor, newColor);
        }

        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
        }

        public static readonly RoutedEvent ColorChangedEvent =
           EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
               typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private void OnColorChanged(Color oldValue, Color newValue)
        {
            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue);
            args.RoutedEvent = ColorPicker.ColorChangedEvent;
            RaiseEvent(args);
        }

        //
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RangeBase slider = GetTemplateChild("PART_RedSlider") as RangeBase;
            //注意使用的是System.Windows.Controls.Primitives.RangeBase类(Slider类继承自该类)而不是Slider类。因为RangeBase类提供了需要的
            //最小功能——在本例中是指Value属性。通过尽可能提高代码的通用性，控件使用者可获得更大自由
            //例如，现在可提供自定义模板，使用不同的派生自RangeBase类的控件代替颜色滑动条

            if (slider != null)
            {
                Binding binding = new Binding("Red");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                slider.SetBinding(RangeBase.ValueProperty, binding);
            }
            slider = GetTemplateChild("PART_GreenSlider") as RangeBase;
            if (slider != null)
            {
                Binding binding = new Binding("Green");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                slider.SetBinding(RangeBase.ValueProperty, binding);
            }
            slider = GetTemplateChild("PART_BlueSlider") as RangeBase;
            if (slider != null)
            {
                Binding binding = new Binding("Blue");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                slider.SetBinding(RangeBase.ValueProperty, binding);
            }

            SolidColorBrush brush = GetTemplateChild("PART_PreviewBrush") as SolidColorBrush;
            if (brush != null)
            {
                Binding binding = new Binding("Color");
                binding.Source = brush;
                binding.Mode = BindingMode.OneWayToSource;
                this.SetBinding(ColorPicker.ColorProperty, binding);
            }
            //因为SolidColorBrush画刷没有包含SetBinding()方法(该方法是在FrameworkElement类中定义的)。一个比较容易的变通方法是为
            //ColorPicker.Color属性创建绑定表达式，使用指向源方向的单向绑定。这样，当颜色拾取器的颜色改变后，将自动更新画刷
        }
    }
}
