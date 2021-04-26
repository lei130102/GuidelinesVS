using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Validation.ViewModel
{
    public class ValidateModelBase : ViewModelBase, IDataErrorInfo
    {
        public ValidateModelBase()
        { }

        private Dictionary<string, string> dataErrors = new Dictionary<string, string>();
        /// <summary>
        /// 是否验证通过
        /// </summary>
        public bool IsValidated
        {
            get
            {
                if(dataErrors != null && dataErrors.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }
        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                ValidationContext vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;

                List<ValidationResult> res = new List<ValidationResult>();

                Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if(res.Count > 0)
                {
                    if(!dataErrors.ContainsKey(vc.MemberName))
                    {
                        dataErrors.Add(vc.MemberName, "");
                    }
                    return string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                }
                dataErrors.Remove(vc.MemberName);
                return null;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
