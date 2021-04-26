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
    [ExportMetadata("Type", new string[] { "ExceptionValidationRuleViewModel" })]
    public class ExceptionValidationRuleViewModel : ViewModelBase, IPluginViewModel
    {
        public ExceptionValidationRuleViewModel()
        { }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);

                if(string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("该字段不能为空!");
                }
            }
        }
    }
}
