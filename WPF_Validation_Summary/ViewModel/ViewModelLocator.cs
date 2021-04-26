using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation_Summary.Utility;

namespace WPF_Validation_Summary.ViewModel
{
    public class ViewModelLocator
    {
        public object ValidationViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("ValidationViewModel");
            }
        }
    }
}
