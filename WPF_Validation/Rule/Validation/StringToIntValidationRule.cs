using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Validation.Rule.Validation
{
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int i;
            if(int.TryParse(value.ToString(), out i))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "请输入一个有效的数字");
        }
    }
}
