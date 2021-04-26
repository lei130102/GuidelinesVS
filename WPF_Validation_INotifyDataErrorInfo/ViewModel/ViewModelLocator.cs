using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation_INotifyDataErrorInfo.Utility;

namespace WPF_Validation_INotifyDataErrorInfo.ViewModel
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

        public object CustomErrorTypeValidationViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("CustomErrorTypeValidationViewModel");
            }
        }

        public object CrossPropertyValidationViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("CrossPropertyValidationViewModel");
            }
        }
    }
}
