using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

//自定义控件中的命令支持

//WPF提供了实现ICommandSource接口并能引发命令的控件(还提供了一些能够处理命名的控件)

//尽管提供了这项支持，但仍可能遇到这样一种控件：尽管没有实现ICommandSource接口，却希望用在命令模型中，对于这种情况，最容易的选择是处理控件的一个事件，
//并使用代码执行合适的命令。不过还有另一种选择，就是自己构建新控件——内置了命令执行逻辑的控件

namespace WPF_Command
{
    /// <summary>
    /// 自定义的滑动条控件，当他的值发生变化时会触发命令。
    /// 
    /// 实现了ICommandSource接口，定义了Command、CommandTarget以及CommandParameter依赖项属性，并且在内部监视RoutedCommand.CanExecuteChanged事件
    /// 
    /// 尽管代码十分简单，但在大多数情况下并不会使用这种解决方案。
    /// 在WPF中创建自定义控件是一个相当重要的步骤，并且大多数开发人员更喜欢使用模板重新设置已有控件的样式，而不是创建全新的类。然而，如果正在
    /// 从头设计自定义控件，并希望提供命令支持，该例子是值得研究的
    /// </summary>
    public class CommandSlider : Slider, ICommandSource
    {
        public CommandSlider()
            : base()
        {
        }

        /// <summary>
        /// make Command a dependency property so it can be DataBound
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(CommandSlider),
                new PropertyMetadata((ICommand)null, new PropertyChangedCallback(CommandChanged)));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandSlider cs = (CommandSlider)d;
            cs.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        /// <summary>
        /// Add a new command to the Command Property
        /// </summary>
        /// <param name="oldCommand"></param>
        /// <param name="newCommand"></param>
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            //
            if(oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
            AddCommand(oldCommand, newCommand);
        }

        /// <summary>
        /// Remove an old command from the Command Property
        /// </summary>
        /// <param name="oldCommand"></param>
        /// <param name="newCommand"></param>
        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if(this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                //if RoutedCommand
                if(command != null)
                {
                    if(command.CanExecute(CommandParameter, CommandTarget))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
                //if not RoutedCommand
                else
                {
                    if(Command.CanExecute(CommandParameter))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// keep a copy of the handler so it doesn't get garbage collected
        /// </summary>
        private static EventHandler canExecuteChangedHandler;

        /// <summary>
        /// add the command
        /// </summary>
        /// <param name="oldCommand"></param>
        /// <param name="newCommand"></param>
        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if(newCommand != null)
            {
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
            }
        }

        /// <summary>
        /// make CommandParameter a dependency property so it can be DataBound
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(CommandSlider),
                new PropertyMetadata((object)null));

        /// <summary>
        /// make CommandTarget a dependency property so it can be DataBound
        /// </summary>
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                "CommandTarget",
                typeof(IInputElement),
                typeof(CommandSlider),
                new PropertyMetadata((IInputElement)null));

        /// <summary>
        /// if Command is defined, then moving the slider will invoke the command;
        /// otherwise, the slider will behave normally
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if(this.Command != null)
            {
                RoutedCommand command = Command as RoutedCommand;

                if(command != null)
                {
                    command.Execute(CommandParameter, CommandTarget);
                }
                else
                {
                    ((ICommand)Command).Execute(CommandParameter);
                }
            }
        }

        #region ICommandSource
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return (object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }
        #endregion
    }





    [ValueConversion(typeof(string), typeof(int))]
    public class FontStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fontSize = (string)value;
            int intFont;

            try
            {
                intFont = Int32.Parse(fontSize);
                return intFont;
            }
            catch(FormatException e)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(double), typeof(int))]
    public class FontDoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double fontSize = (double)value;
            return (int)fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// CustomControlWithCommand.xaml 的交互逻辑
    /// </summary>
    public partial class CustomControlWithCommand : Window
    {
        public CustomControlWithCommand()
        {
            InitializeComponent();
        }

        public static RoutedCommand FontUpdateCommand = new RoutedCommand();

        /// <summary>
        /// if the Readonly Box is checked, we need to force the CommandManager
        /// to raise the RequerySuggested event.This will cause the Command Source
        /// to query the command to see if it can execute or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadOnlyChecked(object sender, RoutedEventArgs e)
        {
            if(txtBoxTarget != null)
            {
                txtBoxTarget.IsReadOnly = true;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// if the Readonly Box is checked, we need to force the CommandManager
        /// to raise the RequerySuggested event. This will cause the Command Source
        /// to query the command to see if it can execute or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadOnlyUnChecked(object sender, RoutedEventArgs e)
        {
            if(txtBoxTarget != null)
            {
                txtBoxTarget.IsReadOnly = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// The ExecutedRoutedEvent Handler
        /// 
        /// if the command target is the TextBox, changes the fontsize to that of the value passed in through the Command Parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderUpdateExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox source = sender as TextBox;

            if(source != null)
            {
                if(e.Parameter != null)
                {
                    try
                    {
                        if((int)e.Parameter > 0 && (int)e.Parameter <= 60)
                        {
                            source.FontSize = (int)e.Parameter;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("in Command \n Parameter: " + e.Parameter);
                    }
                }
            }
        }

        /// <summary>
        /// The CanExecuteRoutedEvent Handler
        /// 
        /// if the Command Source is a TextBox, then set CanExecute to ture;
        /// otherwise, set it to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderUpdateCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TextBox source = sender as TextBox;

            if(source != null)
            {
                if(source.IsReadOnly)
                {
                    e.CanExecute = false;
                }
                else
                {
                    e.CanExecute = true;
                }
            }
        }
    }
}
