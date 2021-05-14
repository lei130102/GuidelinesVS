using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CustomDatePicker.Utility;

namespace WPF_CustomDatePicker.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "CustomDatePickerViewModel" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomDatePickerViewModel : ViewModelBase, IPluginViewModel
    {
        public CustomDatePickerViewModel()
        {
            YearDT = DateTime.Today;
        }

        private DateTime? _yearDT;
        public DateTime? YearDT
        {
            get
            {
                return _yearDT;
            }
            set
            {
                if(_yearDT != value)
                {
                    _yearDT = value;

                    RaisePropertyChanged(() => YearDT);
                }
            }
        }
    }
}
