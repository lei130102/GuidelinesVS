using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace BookService
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 如果就一个，那么用 Import ，类型使用 IBookService
        /// </summary>
        [ImportMany("BookService")]
        public IEnumerable<IBookService> BookServices { get; set; }

        /// <summary>
        /// 导入属性，这里不区分public还是private
        /// </summary>
        [ImportMany]
        public List<string> InputString { get; set; }

        /// <summary>
        /// 导入无参数方法
        /// </summary>
        [Import]
        public Func<string> methodWithoutPara { get; set; }

        /// <summary>
        /// 导入有参数方法
        /// </summary>
        [Import]
        public Func<int, string> methodWithPara { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            if(this.BookServices != null)
            {
                foreach(var s in this.BookServices)
                {
                    MessageBox.Show(s.GetBookName());
                }
            }

            if(this.InputString != null)
            {
                foreach(var str in this.InputString)
                {
                    MessageBox.Show(str);
                }
            }

            //调用无参数方法
            if(this.methodWithoutPara != null)
            {
                MessageBox.Show(this.methodWithoutPara());
            }

            //调用有参数方法
            if(this.methodWithPara != null)
            {
                MessageBox.Show(this.methodWithPara(3000));
            }

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
