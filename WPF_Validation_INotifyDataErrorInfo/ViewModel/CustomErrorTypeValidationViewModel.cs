using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation_INotifyDataErrorInfo.Server;
using WPF_Validation_INotifyDataErrorInfo.Utility;

namespace WPF_Validation_INotifyDataErrorInfo.ViewModel
{
    /// <summary>
    /// 使用自定义错误类型
    /// </summary>
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "CustomErrorTypeValidationViewModel" })]
    public class CustomErrorTypeValidationViewModel : ViewModelBase, IPluginViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<CustomErrorType>> _validationErrors = new Dictionary<string, ICollection<CustomErrorType>>();
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

        private readonly IService _service;

        public CustomErrorTypeValidationViewModel()
        {
            _service = new Service();
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                ValidateUsername(_username);
            }
        }

        private async void ValidateUsername(string username)
        {
            const string propertyKey = "Username";
            ICollection<CustomErrorType> validationErrors = null;
            bool isValid = await Task<bool>.Run(() => 
            {
                return _service.ValidateUsername(username, out validationErrors);
            }).ConfigureAwait(false);

            if(!isValid)
            {
                /* Update the collection in the dictionary returned by the GetErrors method */
                _validationErrors[propertyKey] = validationErrors;
                /* Raise event to tell WPF to execute the GetErrors method */
                RaiseErrorsChanged(propertyKey);
            }
            else if(_validationErrors.ContainsKey(propertyKey))
            {
                /* Remove all errors for this property */
                _validationErrors.Remove(propertyKey);
                /* Raise event to tell WPF to execute the GetErrors method */
                RaiseErrorsChanged(propertyKey);
            }
        }
    }
}
