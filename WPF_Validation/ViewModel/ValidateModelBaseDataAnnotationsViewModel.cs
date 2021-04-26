using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation.Utility;

namespace WPF_Validation.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "ValidateModelBaseDataAnnotationsViewModel" })]
    public class ValidateModelBaseDataAnnotationsViewModel : ValidateModelBase, IPluginViewModel
    {
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
                if(_userName != value)
                {
                    _userName = value;

                    RaisePropertyChanged(() => UserName);
                }
            }
        }

        private string _userPhone;
        /// <summary>
        /// 用户电话
        /// </summary>
        [Required]
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "用户电话必须为8-11位的数值.")]
        public string UserPhone
        {
            get
            {
                return _userPhone;
            }
            set
            {
                if(_userPhone != value)
                {
                    _userPhone = value;

                    RaisePropertyChanged(() => UserPhone);
                }
            }
        }

        private string _userEmail;
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请填写正确的邮箱地址.")]
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                if(_userEmail != value)
                {
                    _userEmail = value;

                    RaisePropertyChanged(() => UserEmail);
                }
            }
        }
    }
}
