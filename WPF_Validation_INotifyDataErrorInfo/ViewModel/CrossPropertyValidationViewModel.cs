using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation_INotifyDataErrorInfo.Utility;

namespace WPF_Validation_INotifyDataErrorInfo.ViewModel
{
    /// <summary>
    /// 跨属性验证 
    /// </summary>
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "CrossPropertyValidationViewModel" })]
    public class CrossPropertyValidationViewModel : ViewModelBase, IPluginViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private void RaiseErrorsChanged(string propertyName)
        {
            if(ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        #region INotifyDataErrorInfo
        public bool HasErrors
        {
            get
            {
                return _validationErrors.Count > 0;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if(string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
            {
                return null;
            }
            return _validationErrors[propertyName];
        }
        #endregion

        private Int16 _type;
        public Int16 Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                ValidateInterestRate();
            }
        }

        private decimal? _interestRate;
        public decimal? InterestRate
        {
            get
            {
                return _interestRate;
            }
            set
            {
                _interestRate = value;
                ValidateInterestRate();
            }
        }

        private const string dictionaryKey = "InterestRate";
        private const string validationMessage = "You must enter an interest rate.";
        private void ValidateInterestRate()
        {
            /* The InterestRate property must have a value only if the Type property is set to 1 */
            if(_type.Equals(1) && !_interestRate.HasValue)
            {
                if (_validationErrors.ContainsKey(dictionaryKey))
                {
                    _validationErrors[dictionaryKey].Add(validationMessage);
                }
                else
                {
                    _validationErrors[dictionaryKey] = new List<string> { validationMessage };
                }
                RaiseErrorsChanged("InterestRate");
            }
            else if(_validationErrors.ContainsKey(dictionaryKey))
            {
                _validationErrors.Remove(dictionaryKey);
                RaiseErrorsChanged("InterestRate");
            }
        }
    }
}
