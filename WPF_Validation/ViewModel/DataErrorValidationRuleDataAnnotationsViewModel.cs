using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation.Utility;

namespace WPF_Validation.ViewModel
{
    //这边我们使用到了RequiredAttribute、StringLengthAttribute、RegularExpressionAttribute 三项，如果有需要进一步了解 DataAnnotations 的可以参考微软官网：
    //https://msdn.microsoft.com/en-us/library/dd901590(VS.95).aspx
    //用 DataAnnotions 后，Model 的更加简洁，校验也更加灵活。可以叠加组合验证 , 面对复杂验证模式的时候，可以自由的使用正则来验证。
    //默认情况下，框架会提供相应需要反馈的消息内容，当然也可以自定义错误消息内容：ErrorMessage 。
    //这边我们还加了个全局的错误集合收集器 ：dataErrors，在提交判断时候判断是否验证通过。
    //这边我们进一步封装索引器，并且通过反射技术读取当前字段下的属性进行验证。
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "DataErrorValidationRuleDataAnnotationsViewModel" })]
    public class DataErrorValidationRuleDataAnnotationsViewModel : ViewModelBase, IPluginViewModel, IDataErrorInfo
    {
        public DataErrorValidationRuleDataAnnotationsViewModel()
        { }

        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        private string _userPhone;
        /// <summary>
        /// 用户电话
        /// </summary>
        [Required]
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "用户电话必须为8~11位的数值。")]
        public string UserPhone
        {
            get
            {
                return _userPhone;
            }
            set
            {
                _userPhone = value;
            }
        }

        private string _userEmail;
        [Required]
        [StringLength(100, MinimumLength=2)]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请填写正确的邮箱地址.")]
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                _userEmail = value;
            }
        }

        /// <summary>
        /// 表单验证错误集合
        /// </summary>
        private Dictionary<string, string> dataErrors = new Dictionary<string, string>();

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                ValidationContext vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;

                List<ValidationResult> res = new List<ValidationResult>();

                //通过反射技术读取当前字段下的属性进行验证
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
                return null;//表示验证通过
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
