using BankInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bank
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<ICard, IMetadata>> cards { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //下面这几行代码应该封装到一个类中
            var catalog = new DirectoryCatalog("Cards");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            foreach(var c in this.cards)
            {
                if (c.Metadata.CardType == "BankOfChina")
                {
                    MessageBox.Show(c.Value.GetCountInfo());
                }
            }

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
