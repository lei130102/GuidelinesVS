using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Validation.Utility;

namespace WPF_Validation.ViewModel
{
    /// <summary>
    /// 继承IDataErrorInfo接口后，实现方法两个属性：Error 属性用于指示整个对象的错误，而索引器用于指示单个属性级别的错误。
    /// 每次的属性值发生变化，则索引器进行一次检查，看是否有验证错误的信息返回。
    /// 两者的工作原理相同：如果返回非 null 或非空字符串，则表示存在验证错误。否则，返回的字符串用于向用户显示错误。 
    /// </summary>
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new string[] { "DataErrorValidationRuleViewModel" })]
    public class DataErrorValidationRuleViewModel : ViewModelBase, IPluginViewModel, IDataErrorInfo
    {
        public DataErrorValidationRuleViewModel()
        {
            //需要设置不为空默认值，否则直接算作验证不通过
            this.UserName = "abc";
        }

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
            }
        }

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "UserName":
                        if(string.IsNullOrEmpty(this.UserName))
                        {
                            return "用户名不能为空";
                        }
                        break;
                }
                return string.Empty;
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
