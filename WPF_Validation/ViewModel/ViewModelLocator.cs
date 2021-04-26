using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation.Utility;

namespace WPF_Validation.ViewModel
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

        public object ValidationShowViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("ValidationShowViewModel");
            }
        }

        public object CustomValidationRuleViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("CustomValidationRuleViewModel");
            }
        }

        public object ExceptionValidationRuleViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("ExceptionValidationRuleViewModel");
            }
        }

        public object DataErrorValidationRuleViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("DataErrorValidationRuleViewModel");
            }
        }

        public object DataErrorValidationRuleDataAnnotationsViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("DataErrorValidationRuleDataAnnotationsViewModel");
            }
        }

        public object ValidateModelBaseDataAnnotationsViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("ValidateModelBaseDataAnnotationsViewModel");
            }
        }
    }
}
