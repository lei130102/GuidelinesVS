using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Basic.Utility;

namespace WPF_MVVM_Basic.ViewModel
{
    public class ViewModelLocator
    {
        public object Song
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("歌曲");
            }
        }
    }
}
