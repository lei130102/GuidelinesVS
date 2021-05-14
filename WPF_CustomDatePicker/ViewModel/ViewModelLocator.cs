using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CustomDatePicker.Utility;

namespace WPF_CustomDatePicker.ViewModel
{
    public class ViewModelLocator
    {
        public object CustomDatePickerViewModel
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("CustomDatePickerViewModel");
            }
        }
    }
}
