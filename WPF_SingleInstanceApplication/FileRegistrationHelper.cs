using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//为测试单实例应用程序，需要使用Windows注册文件扩展名(.testDoc)，并将其与应用程序相关联。这样，当单击.testDoc文件时，应用程序将立即启动

//创建文件类型注册的
//方法1:使用Windows资源管理器手动注册：
//1.右击.testDoc文件，然后选择Open With | Choose Default Program菜单项
//2.在Open With对话框中单击Browse按钮，找到应用程序的.exe文件并双击
//3.如果不希望使应用程序默认处理这种文件类型，务必在Open With对话框中取消选中Always use the selected program to open this type file选项。在
//这种情况下，不能通过双击文件启动应用程序。然而，可通过右击文件，选择Open With菜单项，并从列表中选择应用程序来打开
//4.单击OK按钮
//方法2:运行编辑注册表代码
//FileRegistrationHelper.SetFileAssociation

namespace WPF_SingleInstanceApplication
{
    //使用Microsoft.Win32命名空间中的类注册.testDoc文件扩展名
    //注册过程只需执行一次。注册后，每次双击扩展名为.testDoc的文件时，就会启动SingleInstanceApplication，而且文件作为命令行参数进行传递
    //如果SingleInstanceApplication程序已开始运行，就会调用SingleInstanceApplicaitonWrapper.OnStartupNextInstance()方法，并通过已经
    //运行的应用程序加载新文档
    public class FileRegistrationHelper
    {
        public static void SetFileAssociation(string extension, string progID)
        {
            //Create extension subkey
            SetValue(Registry.ClassesRoot, extension, progID);

            //Create progid subkey
            string assemblyFullPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"\");

            StringBuilder sbShellEntry = new StringBuilder();
            sbShellEntry.AppendFormat("\"{0}\" \"%1\"", assemblyFullPath);

            SetValue(Registry.ClassesRoot, progID + @"\shell\open\command", sbShellEntry.ToString());

            StringBuilder sbDefaultIconEntry = new StringBuilder();
            sbDefaultIconEntry.AppendFormat("\"{0}\",0", assemblyFullPath);

            SetValue(Registry.ClassesRoot, progID + @"\DefaultIcon", sbDefaultIconEntry.ToString());

            //Create application subkey
            SetValue(Registry.ClassesRoot, @"Applications\" + Path.GetFileName(assemblyFullPath), "", "NoOpenWith");
        }

        private static void SetValue(RegistryKey root, string subKey, object keyValue)
        {
            SetValue(root, subKey, keyValue, null);
        }

        private static void SetValue(RegistryKey root, string subKey, object keyValue, string valueName)
        {
            bool hasSubKey = ((subKey != null) && (subKey.Length > 0));
            RegistryKey key = root;

            try
            {
                if (hasSubKey)
                {
                    key = root.CreateSubKey(subKey);
                }
                key.SetValue(valueName, keyValue);
            }
            finally
            {
                if(hasSubKey && (key != null))
                {
                    key.Close();
                }
            }
        }
    }
}

//文件注册任务通常由安装程序执行。在应用程序代码中包含该任务的常见问题是，运行应用程序的用户需要提升权限，而用户往往并不拥有这些权限。事实上，即使用户
//拥有这些权限，Windows User Account Control(UAC, 用户账户控制)特性仍会阻止代码对其进行访问

//为了理解这一点，需要认识到，在UAC看来，所有应用程序都具有三种运行级别之一。通常，应用程序使用asInvoker运行级别。要请求管理员级别的权限，必须右击应用程序
//的可执行文件，从弹出菜单中选择Run As Administrator菜单项，以便在启动时获得该权限。当使用Visual Studio测试应用程序时，为了获得管理员权限，必须右击
//Visual Studio快捷方式并选择Run As Administrator菜单项

//如果应用程序需要管理员权限，可通过使用requireAdministrator运行级别要求他们，或者使用highestAvailable运行级别要求管理员权限。对于任何一种方式，都需要
//创建应用程序清单文件——清单文件包含可嵌入到编译过的程序集中的XML块。要改变运行级别，只需要修改清单文件中的<requestedExecutionLevel>元素的级别特性。
//有效值为asInvoker、requireAdministrator以及highestAvailable

//在有些情况下，可能希望在特定情形下请求管理员权限。在文件注册示例中，仅当应用程序第一次运行并且需要创建注册时，才选择请求管理员权限，从而避免不必要的UAC警告.
//实现该模式的最简单方法是在单独的可执行程序中放置需要更高权限的代码，然后在需要时调用这个可执行程序
