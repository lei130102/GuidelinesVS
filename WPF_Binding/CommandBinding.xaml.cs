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
using System.Windows.Shapes;



//WPF命令简介
//在本教程的前一章中，我们讨论了如何处理事件，例如当用户点击按钮或菜单项时。 然而在现代的用户界面中，通常从多个位置访问函数，由不同的用户动作调用。

//例如，如果您有一个带有主菜单和一组工具栏的典型界面，则可以在菜单，工具栏，上下文菜单(例如，在主应用程序区域中单击鼠标右键时)使用新建(New)或打开(Open)等操作，以及使用键盘快捷键，如Ctrl+N和Ctrl+O.

//对应上面的每种行为的响应代码都完全一样，但是在WinForms 应用程序中，你不得不为每一种行为定义一个对应的event 然后调用相同的方法。在上面的例子中，将会最少产生三个事件处理，另外加上处理键盘快捷键的代码。这不是一种理想的处理方法。

//命令
//在WPF中，微软尝试使用命令这个概念来解决这个问题。它允许在一个地方定义命令，并且在所有的用户接口控件之中调用这些命令，例如menu，ToolBar 按钮等等。WPF会监听键盘快捷键，并且如果存在合适的命令，会直接调用，这使得命令成为一个理想的在应用中提供快捷键的方式。

//在处理同一函数的多个入口时，命令也解决了另一个麻烦。在WinForms应用程序中，您将负责编写可在操作不可用时禁用用户界面元素的代码。例如，如果您的应用程序能够使用剪切命令（如Cut），但仅在选择了文本时，您必须在每次文本选择更改时手动启用和禁用主菜单项，工具栏按钮和上下文菜单项。

//使用WPF命令，这是集中的。使用一种方法，您可以决定是否可以执行给定命令，然后WPF自动打开或关闭所有订阅界面元素。这使得创建响应式动态应用程序变得更加容易！

//命令绑定
//命令实际上并没有自己做任何事情。在根目录中，它们由ICommand接口组成，该接口仅定义一个事件和两个方法：Execute()和CanExecute()。第一个用于执行实际操作，而第二个用于确定操作当前是否可用。要执行命令的实际操作，您需要命令和代码之间的链接，这是CommandBinding发挥作用的地方。

//CommandBinding通常在Window或UserControl上定义，并保存对它处理的Command的引用，以及用于处理Command的Execute()和CanExecute()事件的实际事件处理程序。

//预定义的命令
//您当然可以实现自己的命令，我们将在下一章中介绍，但为了使您更容易，WPF团队已定义了100多个常用命令，您可以使用它们。它们分为5类，分别称为ApplicationCommands，NavigationCommands，MediaCommands，EditingCommands和ComponentCommands。特别是ApplicationCommands包含很多常用操作的命令，如New，Open，Save and Cut，Copy和Paste。

//小结
//命令可帮助您使用单个事件处理程序响应来自多个不同来源的常见操作。它还使基于当前可用性和状态启用和禁用用户界面元素变得更加容易。这完全是理论，但在接下来的章节中，我们将讨论如何使用命令以及如何定义自己的自定义命令。

namespace WPF_Binding
{
    /// <summary>
    /// CommandBinding.xaml 的交互逻辑
    /// </summary>
    public partial class CommandBinding : Window
    {
        public CommandBinding()
        {
            InitializeComponent();
        }

        private void newCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The New command was invoked");
        }

        private void newCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Cut();
        }

        private void cutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txtEditor != null) && (txtEditor.SelectionLength > 0);
        }

        private void pasteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Paste();
        }

        private void pasteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void exitCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void exitCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }

    //
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand(
            "Exit"
            ,"Exit"
            ,typeof(CustomCommands)
            ,new InputGestureCollection() { 
                new KeyGesture(Key.F4, ModifierKeys.Alt)
            });

        //Define more commands here, just like the one above
    }
}


//我们通过将命令绑定到其CommandBindings集合来定义Window上的命令绑定。我们指定我们希望使用的Command（来自ApplicationCommands的New命令），以及两个事件处理程序。可视界面由一个按钮组成，我们将命令附加到Command属性。

//在Code-behind中，我们处理这两个事件。当应用程序空闲以查看特定命令当前是否可用时，WPF将调用的CanExecute处理程序对于此示例非常简单，因为我们希望此特定命令始终可用。这是通过将事件参数的CanExecute属性设置为true来完成的。

//Executed处理程序在调用命令时只显示一个消息框。如果您运行示例并按下按钮，您将看到此消息。需要注意的是，此命令定义了一个默认的键盘快捷键，您可以获得额外的奖励。您可以尝试按键盘上的Ctrl + N而不是单击按钮 - 结果是相同的。







//在第一个示例中，我们实现了一个简单返回true的CanExecute事件，以便该按钮始终可用。但是，对于所有按钮当然不是这样 - 在许多情况下，您希望根据应用程序中的某种状态启用或禁用按钮。

//一个非常常见的例子是切换使用Windows剪贴板的按钮，您希望仅在选择文本时启用剪切和复制按钮，并且只有在剪贴板中存在文本时才启用粘贴按钮。这正是我们在这个例子中将要完成的事情：

//所以，我们有一个非常简单的界面，有几个按钮和一个TextBox控件。第一个按钮将切入剪贴板，第二个按钮将从中粘贴。

//在Code-behind中，每个按钮有两个事件：一个执行实际操作，一个名称以_Executed结尾，然后是CanExecute事件。在每个中，您将看到我应用一些逻辑来决定是否可以执行该操作，然后将其分配给EventArgs上的返回值CanExecute。

//关于这一点很酷的是，您不必调用这些方法来更新按钮 - 当应用程序有空闲时，WPF会自动执行此操作，确保您的界面始终保持更新状态。






//实现自定义WPF命令
//在上一章中，我们研究了使用WPF中已定义的命令的各种方法，当然您也可以实现自己的命令。它非常简单，一旦你完成它，就可以使用自己的命令，就像在WPF中定义的命令一样。

//开始实现自己的命令的最简单方法是使用包含它们的静态类。然后将每个命令作为static字段添加到此类，允许您在应用程序中使用它们。由于WPF由于一些奇怪的原因，没有实现退出/离开命令，我决定为我们的自定义命令示例实现一个。它看起来像这样：

//在标记中，我定义了一个带菜单和按钮的非常简单的界面，它们都使用我们新的自定义Exit命令。此命令在我们自己的CustomCommands类中的代码隐藏中定义，然后在窗口的CommandBindings集合中引用，我们在其中分配应该用于执行/检查是否允许执行的事件。

//所有这些都与前一章中的示例类似，只是我们从我们自己的代码引用命令（使用顶部定义的“self”命名空间）而不是内置命令。

//在Code-behind中，我们响应命令的两个事件：一个事件只允许命令一直执行，因为对于exit / quit命令通常是这样，而另一个事件调用Shutdown方法将终止我们的命令应用。一切都很简单。

//如前所述，我们将Exit命令实现为静态CustomCommands类的字段。有几种方法可以在命令上定义和分配属性，但是我选择了更紧凑的方法（如果放在同一条线上会更紧凑，但为了便于阅读，我在这里添加了换行符）所有这一切都通过构造函数。参数是命令的文本/标签，命令的名称，所有者类型，然后是InputGestureCollection，允许我为命令定义默认快捷方式（Alt + F4）。

