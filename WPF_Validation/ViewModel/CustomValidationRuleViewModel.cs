using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation.Utility;

namespace WPF_Validation.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "CustomValidationRuleViewModel" })]
    public class CustomValidationRuleViewModel : ViewModelBase, IPluginViewModel
    {
        public CustomValidationRuleViewModel()
        { }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if(_age != value)
                {
                    _age = value;

                    RaisePropertyChanged(() => Age);
                }
            }
        }
    }
}
