using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_RadioButton.Model;
using WPF_RadioButton.Utility;

namespace WPF_RadioButton.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "RadioButtonViewModel" })]
    class RadioButtonViewModel : ViewModelBase, IPluginViewModel
    {
        private Sex _sex;
        public Sex Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                if(_sex != value)
                {
                    _sex = value;

                    RaisePropertyChanged(() => Sex);
                }
            }
        }
    }
}
