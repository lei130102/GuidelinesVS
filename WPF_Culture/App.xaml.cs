using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;




//特定格式化
//如果您只需要为特定信息应用格式，例如 单个Label控件的内容，您可以使用ToString()方法和CultureInfo类的组合，在运行中轻松完成此操作。 例如，在上面的示例中，我应用了不同的基于区域性的格式，如下所示：

//double largeNumber = 123456789.42;

//CultureInfo usCulture = new CultureInfo("en-US");
//CultureInfo deCulture = new CultureInfo("de-DE");
//CultureInfo seCulture = new CultureInfo("sv-SE");

//lblNumberUs.Content = largeNumber.ToString("N2", usCulture);
//lblNumberDe.Content = largeNumber.ToString("N2", deCulture);
//lblNumberSe.Content = largeNumber.ToString("N2", seCulture);
//对于某些情况，这可能就足够了，您需要在几个地方进行特殊格式化，但一般情况下，您应该决定应用程序是否应该使用系统设置（默认设置），
//或者是否应该使用特定的区域性设置覆盖此行为 对于整个应用程序。






//CurrentCulture和CurrentUICulture
//将另一种区域性应用于WPF应用程序非常简单。 您可能会处理Thread类的CurrentThread属性中的两个属性：CurrentCulture和CurrentUICulture。 但有什么区别？

//CurrentCulture属性控制数字和日期等的格式。 默认值来自执行应用程序的计算机的操作系统，可以独立于其操作系统使用的语言进行更改。 例如，对于居住在德国的人来说，
//安装带有英语作为界面语言的Windows非常普遍，同时仍然更喜欢使用德语表示数字和日期。 对于这种情况，CurrentCulture属性将默认为德语。

//CurrentUICulture属性指定界面应使用的语言。 这仅在您的应用程序支持多种语言时才有意义，例如： 通过使用语言资源文件。 再次，这允许您使用一种区域性语言（例如英语），
//在处理数字，日期等的输入/输出时使用另一个（例如德语）。

//改变应用程序区域性
//考虑到这一点，您现在必须决定是否更改CurrentCulture和/或CurrentUICulture。 它可以在任何时候完成，但在启动应用程序时最有意义 - 否则，在切换之前，
//某些输出可能已经使用默认区域性生成。 这是一个示例，我们在Application_Startup()事件中更改Culture和UICulture，它可以在WPF应用程序的App.xaml.cs文件中使用：




namespace WPF_Culture
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //有了这个，数字和日期现在将根据他们在德语(de - DE)中的喜好来格式化。 如上所述，如果您的应用程序不支持多种语言，您可以省略定义CurrentUICulture的行（最后一行）。

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
