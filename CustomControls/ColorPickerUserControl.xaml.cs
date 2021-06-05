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

namespace CustomControls
{
    //创建颜色拾取器的第一步是为自定义控件库项目添加用户控件(UserControl)。当添加用户控件后，Visual Studio会创建XAML标记文件和相应的
    //包含初始化代码与事件处理代码的自定义类。这与创建新的窗口或页面是相同的——唯一的区别在于顶级容器是UserControl类：

    /// <summary>
    /// ColorPickerUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickerUserControl : UserControl
    {
        public ColorPickerUserControl()
        {
            InitializeComponent();
            SetUpCommands();
        }


        private void SetUpCommands()
        {
            // Set up command bindings.
            //CommandBinding binding = new CommandBinding(ApplicationCommands.Undo, 
            // UndoCommand_Executed, UndoCommand_CanExecute);

            // this.CommandBindings.Add(binding);
        }

        private Color? previousColor;
        private static void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;
            e.CanExecute = colorPicker.previousColor.HasValue;
        }
        private static void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Use simple reverse-or-redo Undo (like Notepad).
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;
            colorPicker.Color = (Color)colorPicker.previousColor;
        }

        //为了属性定义静态字段只是第一步，还需要有静态构造函数，用于在用户控件中注册这些依赖项属性，指定属性的名称、数据类型以及拥有属性的控件类
        //正如之前介绍的依赖项属性，可通过传递具有正确标志设置的FrameworkPropertyMetadata对象，在静态构造函数中指定选择的特定属性特性(如值继承)
        //还可指出在什么地方为验证、数据强制以及属性更改通知关联回调函数
        //在颜色拾取器中，只需要考虑一个因素——当各种属性变化时需要关联回调函数进行响应。因为Red、Green和Blue属性实际上是Color属性的不同表示，并且
        //如果一个属性发生变化，就需要确保其他属性保持同步
        static ColorPickerUserControl()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));

            RedProperty = DependencyProperty.Register("Red", typeof(byte),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            GreenProperty = DependencyProperty.Register("Green", typeof(byte),
                typeof(ColorPickerUserControl),
                    new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            BlueProperty = DependencyProperty.Register("Blue", typeof(byte),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            //命令支持
            //许多控件具有命令支持。可使用以下两种方法为自定义控件添加命令支持：
            //1.添加将控件链接到特定命令的命令绑定。通过这种方法，控件可以响应命令，而且不需要借助于任何外部代码
            //2.为命令创建新的RoutedUICommand对象，作为自定义控件的静态字段。然后为这个命令对象添加命令绑定。这种方法可使自定义控件自动支持
            //没有在基本命令类集合中定义的命令

            //本例使用的是第一种方法

            //在颜色拾取器中为了支持Undo功能，需要使用成员字段跟踪以前选择的颜色：
            //private Color? previousColor;
            //将该字段设置为可空是合理的，因为当第一次创建控件时，还没有设置以前选择的颜色(也可在某个希望使其不能逆转的操作之后，通过代码清除以前
            //的颜色)
            //当颜色发生变化时，只需要记录旧值。可通过在OnColorChanged()方法的最后添加以下代码行来达到该目的：
            //colorPicker.previousColor = (Color)e.OldValue;
            //现在已经具备了支持Undo命令需要的基础架构。剩余的工作室创建将控件链接到命令以及处理CanExecute和Executed事件的命令绑定

            //第一次创建控件时是创建命令绑定的最佳时机。例如，下面的代码使用颜色拾取器的构造函数为ApplicationCommands.Undo命令添加命令绑定：
            //public ColorPicker()
            //{
            //  InitializeComponent();
            //  SetUpCommands();
            //}
            //private void SetUpCommands()
            //{
            //  //Set up command bindings.
            //  CommandBinding binding = new CommandBinding(ApplicaitonCommands.Undo, UndoCommand_Executed, UndoCommand_CanExecute);
            //  this.CommandBindings.Add(binding);
            //}
            //为使命令奏效，需要处理CanExecute事件，并且只要有以前的颜色值就允许执行命令
            //private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            //{
            //    e.CanExecute = previousColor.HasValue;
            //}
            //最后，当执行命令后，可交换新的颜色：
            //private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            //{
            //  this.Color = (Color)previousColor;
            //}
            //可通过两种不同方式触发Undo命令。当用户控件中的某个元素具有焦点时，可以使用默认的Ctrl+Z组合键绑定，也可为客户添加用于触发命令的按钮
            //<Button Commmand="Undo" CommandTarget="{Binding ElementName=colorPicker}">
            //    Undo
            //</Button>
            //这两种方式都会丢弃当前颜色并应用以前的颜色

            //(提示：当前示例只存储了一个级别的撤销信息。然而，很容易就能创建存储一系列值的撤销堆栈。需要做的只是在适当类型的集合中存储Color值。
            //System.Collections.Generic命令空间中的Stack集合是不错的选择，因为该集合实现了后进先出的方法，当执行撤销操作时，可以很容易地通过
            //该方法获取最近的Color对象)

            //更可靠的命令
            //前面描述的技术是将命令链接到控件的相当合理的方法，但这不是在WPF元素和专业控件中使用的技术。这些元素使用更可靠的方法，并使用
            //CommandManager.RegisterClassCommandBinding()方法关联静态的命令处理程序

            //之前实现存在问题：使用公共CommandBindings集合。这使得命令比较脆弱，因为客户可自由地修改CommandBindings集合。而使用
            //RegisterClassCommandBinding()方法无法做到这一点。WPF控件使用的就是这种方法。例如，如果查看TextBox的CommandBindings集合，
            //不会发现任何用于硬编码命令的绑定，例如Undo、Redo、Cut、Copy以及Paste等命令，因为他们被注册为类绑定

            //这种技术非常简单。不在实例构造函数中创建命令绑定，而必须在静态构造函数中创建命令绑定

            CommandManager.RegisterClassCommandBinding(typeof(ColorPickerUserControl),
                new CommandBinding(ApplicationCommands.Undo,
                UndoCommand_Executed, UndoCommand_CanExecute));

            //尽管上面的代码变化不大，但有一个重要变化。因为UndoCommand_Executed()和UndoCommand_CanExecute()方法是在构造函数中引用的，所以他们
            //必然是静态方法。为检索实例数据(如当前颜色和以前颜色的信息)，需要将事件发送者转换为ColorPicker对象，并使用该对象

            //下面是修改之后的命令处理代码
            //private static void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            //{
            //    ColorPicker colorPicker = (ColorPicker)sender;
            //    e.CanExecute = colorPicker.previousColor.HasValue;
            //}
            //private static void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
            //{
            //    ColorPicker colorPicker = (ColorPicker)sender;
            //    Color currentColor = colorPicker.Color;
            //    colorPicker.Color = (Color)colorPicker.previousColor;
            //}
            //此外，这种技术不局限于命令。如果希望将事件处理逻辑硬编码到自定义控件，可通过EventManager.RegisterClassHandler()方法使用类
            //事件处理程序。类事件处理程序总在实例事件处理程序之前调用，从而允许开发人员很容易地抑制事件
        }

        //最简单的起见是设计用户控件对外界公开的公共接口。换句话说，就是设计控件使用者(使用控件的应用程序)使用的与颜色拾取器进行交互的属性、方法和事件
        //最基本的细节是Color属性——毕竟，颜色拾取器不过是用于显示和选择颜色值的特定工具。为支持WPF特性，如数据绑定、样式以及动画，控件的可写属性几乎都是
        //依赖性属性
        public static DependencyProperty ColorProperty;

        //添加标准的属性封装器，使访问他们变得更加容易，并可在XAML中使用他们
        //注意，属性封装器不能包含任何逻辑，因为可直接使用DependencyObject基类的SetValue()和GetValue()方法设置和检索属性(例如在本例中，属性同步机制
        //是使用回调函数实现的，当属性发生变化时通过属性封装器或者直接调用SetValue()方法引发回调函数)
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

        //Color属性将允许控件使用者通过代码设置或检索颜色值。然而，颜色拾取器中的滑动条控件也允许用户修改当前颜色的一个方面。为实现这一设计，当滑动条
        //的值发生变化时，需要使用事件处理程序进行响应，并且相应地更新Color属性。但使用数据绑定关联滑动条会更加清晰。为使用数据绑定，需要将每个颜色
        //成分定义为单独的依赖项属性
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        //添加标准的属性封装器，使访问他们变得更加容易，并可在XAML中使用他们
        //注意，属性封装器不能包含任何逻辑，因为可直接使用DependencyObject基类的SetValue()和GetValue()方法设置和检索属性(例如在本例中，属性同步机制
        //是使用回调函数实现的，当属性发生变化时通过属性封装器或者直接调用SetValue()方法引发回调函数)
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        //添加标准的属性封装器，使访问他们变得更加容易，并可在XAML中使用他们
        //注意，属性封装器不能包含任何逻辑，因为可直接使用DependencyObject基类的SetValue()和GetValue()方法设置和检索属性(例如在本例中，属性同步机制
        //是使用回调函数实现的，当属性发生变化时通过属性封装器或者直接调用SetValue()方法引发回调函数)
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        //添加标准的属性封装器，使访问他们变得更加容易，并可在XAML中使用他们
        //注意，属性封装器不能包含任何逻辑，因为可直接使用DependencyObject基类的SetValue()和GetValue()方法设置和检索属性(例如在本例中，属性同步机制
        //是使用回调函数实现的，当属性发生变化时通过属性封装器或者直接调用SetValue()方法引发回调函数)
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        //属性变化回调函数负责使Color属性与Red、Green以及Blue属性保持一致。无论何时Red、Green以及Blue属性发生变化，都会相应地调整Color属性，当
        //设置Color属性时，也会更新Red、Green和Blue值
        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;

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
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
        }
        //尽管很明显，但当各个属性试图改变其他属性时，上面的代码不会引起一系列无休止的调用。因为WPF不允许重新进入属性变化回调函数。例如，如果改变
        //Color属性，就会触发OnColorChanged()方法。OnColorChanged()方法会修改Red、Green以及Blue属性，从而触发OnColorRGBChanged()回调方法三次
        //(每个属性触发一次)。然而OnColorRGBChanged()方法不会再次触发OnColorChanged()方法

        //提示：您可能使用之前介绍的强制回调函数来处理颜色属性。然而，这种方法不合适。属性强制回调函数是针对不相关的并且可以相互覆盖或影响的属性
        //设计的。他们对于以不同方式提供相同数据的属性是没有意义的。如果在本例中使用属性强制，可能会为Red、Green以及Blue属性设置不同的值，并且具有
        //覆盖Color属性的颜色信息。真正需要的行为是设置Red、Green和Blue属性，并使用这些颜色信息永久地改变Color属性的值














        //可能还希望添加路由事件，当发生一些事情时用于通知控件使用者。
        //在本示例中，当颜色发生变化后，触发一个事件是很有用处的。尽管可将这个事件定义为普通的.NET事件，但使用路由事件可提供事件冒泡和隧道特性，从而
        //可在更高层次的父元素(如包含窗口)中处理事件

        //在静态构造函数中注册事件。在静态构造函数中指定事件的名称、路由策略、签名以及拥有事件的类

        //不一定要为事件签名创建新的委托，有时可重用已经存在的委托。两个有用的委托是：
        //RoutedEventHandler(用于不带有额外信息的路由事件)
        //RoutedPropertyChangedEventHandler(用于提供属性发生变化之后的旧值和新值的路由事件)
        //本例使用的RoutedPropertyChangedEventHandler委托，是被类型参数化了的泛型委托。所以，可为任何属性数据类型使用该委托，而不会牺牲类型安全功能
        public static readonly RoutedEvent ColorChangedEvent =
           EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
               typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPickerUserControl));

        //定义并注册事件后，需要创建标准的.NET事件封装器来公开事件。事件封装器可用于关联和删除事件监听程序
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        //最后的细节是在适当时候引发事件的代码。该代码必须调用继承自DependencyObject基类的RaiseEvent()方法
        //注意，无论何时修改Color属性，不管是直接修改还是通过修改Red、Green以及Blue成分，都会触发OnColorChanged()静态回调函数
        private void OnColorChanged(Color oldValue, Color newValue)
        {
            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue);
            args.RoutedEvent = ColorPickerUserControl.ColorChangedEvent;
            RaiseEvent(args);
        }
    }
}
