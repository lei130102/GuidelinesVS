using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation_Summary.Utility;

namespace WPF_Validation_Summary.ViewModel
{
    public class ValidationModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<ValidationErrorType>> _validationErrors = new Dictionary<string, ICollection<ValidationErrorType>>();

        private void RaiseErrorsChanged(string propertyName)
        {
            if(ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

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

        protected void Validation(string propertyName)
        {
            ValidationContext vc = new ValidationContext(this, null, null);
            vc.MemberName = propertyName;

            List<ValidationResult> res = new List<ValidationResult>();

            Validator.TryValidateProperty(this.GetType().GetProperty(propertyName).GetValue(this, null), vc, res);
            if(res.Count > 0)
            {
                string temp = string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                /* 更新GetErrors方法返回值 */
                /* 因为本例中限制界面只显示一条验证错误信息，所以这里将所有验证错误信息合并成一个 */
                ICollection<ValidationErrorType> validationErrors = new List<ValidationErrorType>();
                validationErrors.Add(new ValidationErrorType(string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray()), Severity.ERROR));
                _validationErrors[propertyName] = validationErrors;

                /* 触发事件，使WPF执行GetErrors方法 */
                RaiseErrorsChanged(propertyName);
            }
            else if(_validationErrors.ContainsKey(propertyName))
            {
                /* 移除这个属性相关的所有验证错误信息 */
                _validationErrors.Remove(propertyName);
                /* 触发事件，使WPF执行GetErrors方法 */
                RaiseErrorsChanged(propertyName);
            }
        }
    }
}
