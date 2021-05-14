using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_RadioButton.Utility;

namespace WPF_RadioButton.ViewModel
{
    public class ViewModelLocator
    {
        public object RadioButtonViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("RadioButtonViewModel");
            }
        }
    }
}
