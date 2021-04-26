using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Validation_Summary.Utility
{
    public class ValidationErrorType
    {
        public ValidationErrorType(string validationMessage, Severity severity)
        {
            this.ValidationMessage = validationMessage;
            this.Severity = severity;
        }

        public string ValidationMessage { get; private set; }
        public Severity Severity { get; private set; }
    }
}
