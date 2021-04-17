using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

//1.向项目添加 App.config 文件：
//右击项目名称，选择“添加”→“添加新建项”，在出现的“添加新项”对话框中，选择“添加应用程序配置文件”；
//如果项目以前没有配置文件，则默认的文件名称为“app.config”，单击“确定”

//2.App.config 文件中的 connectionStrings 配置节点

//3.App.config 文件中的 appSettings 配置节点

namespace Config
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            {
                //读取 connectionStrings 配置节点
                string wz = ConfigurationManager.ConnectionStrings["wz"].ConnectionString;
            }

            {
                //更新 connectionStrings 配置节点
                bool isModified = false;
                if(ConfigurationManager.ConnectionStrings["wz"] != null)
                {
                    isModified = true;
                }

                string newName = "newName";
                string newConString = "newConString";
                string newProviderName = "newProviderName";
                ConnectionStringSettings newSetting = new ConnectionStringSettings(newName, newConString, newProviderName);

                //打开可执行文件对应的配置文件
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if(isModified)
                {
                    config.ConnectionStrings.ConnectionStrings.Remove(newName);
                }
                config.ConnectionStrings.ConnectionStrings.Add(newSetting);
                //保存对配置文件所作的更改
                config.Save(ConfigurationSaveMode.Modified);
                //强制重新载入配置文件的 ConnectionStrings 配置节点
                ConfigurationManager.RefreshSection("ConnectionStrings");
            }

            {
                //读取 appSettings 配置节点

                foreach(string key in ConfigurationManager.AppSettings)
                {
                    if(key == "key1")
                    {
                        string value = ConfigurationManager.AppSettings[key];
                    }
                }
            }

            {
                //更新 connectionStrings 配置节点

                bool isModified = false;
                foreach(string key in ConfigurationManager.AppSettings)
                {
                    if(key == "key1")
                    {
                        isModified = true;
                    }
                }
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if(isModified)
                {
                    config.AppSettings.Settings.Remove("key1");
                }
                config.AppSettings.Settings.Add("key1", "value1_1");
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

            MainWindow window = new MainWindow();
            window.Show();
        }

        public static void SaveConfig(string Key, string Value)
        {
            XmlDocument doc = new XmlDocument();
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "App.config";
            doc.Load(strFileName);
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for(int i = 0; i < nodes.Count; ++i)
            {
                XmlAttribute att = nodes[i].Attributes["key1"];
                if(att.Value == Key)
                {
                    att = nodes[i].Attributes["value"];
                    att.Value = Value;
                    break;
                }
            }
            doc.Save(strFileName);
        }
    }
}
