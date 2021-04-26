using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPF_Validation_INotifyDataErrorInfo.Server
{
    public enum Severity
    {
        WARNING,
        ERROR
    }

    public class CustomErrorType
    {
        public CustomErrorType(string validationMessage, Severity severity)
        {
            this.ValidationMessage = validationMessage;
            this.Severity = severity;
        }

        public string ValidationMessage { get; private set; }
        public Severity Severity { get; private set; }
    }
    public interface IService
    {
        bool ValidateUsername(string username, out ICollection<string> validationErrors);

        bool ValidateUsername(string username, out ICollection<CustomErrorType> validationErrors);
    }

    public class Service : IService
    {
        public bool ValidateUsername(string username, out ICollection<string> validationErrors)
        {
            validationErrors = new List<string>();
            int count = 0;

            //正确代码，测试不方便所以注释掉
            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Users] WHERE Username=@Username", conn);
            //    cmd.Parameters.Add("@Username", SqlDbType.VarChar);
            //    cmd.Parameters["@Username"].Value = username;
            //    conn.Open();
            //    count = (int)cmd.ExecuteScalar();
            //}

            if(count > 0)
            {
                validationErrors.Add("The supplied username is already in use.Please choose another one.");
            }

            /* Verifying that length of username */
            if(username.Length > 10 || username.Length < 4)
            {
                validationErrors.Add("The username must be between 4 and 10 characters long.");
            }

            /* Verifying that the username contains only letters */
            if(!Regex.IsMatch(username, @"^[a-zA-Z]+$"))
            {
                validationErrors.Add("The username must only contain letters (a-z, A-Z).");
            }

            return validationErrors.Count == 0;
        }

        /* The service method modifed to return objects of type CustomErrorType instead of System.String */
        public bool ValidateUsername(string username, out ICollection<CustomErrorType> validationErrors)
        {
            validationErrors = new List<CustomErrorType>();
            int count = 0;

            //正确代码，测试不方便所以注释掉
            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Users] WHERE Username=@Username", conn);
            //    cmd.Parameters.Add("@Username", SqlDbType.VarChar);
            //    cmd.Parameters["@Username"].Value = username;
            //    conn.Open();
            //    count = (int)cmd.ExecuteScalar();
            //}

            if (count > 0)
            {
                validationErrors.Add(new CustomErrorType("The supplied username is already in use.Please choose another one.", Severity.ERROR));
            }

            /* Verifying that length of username */
            if (username.Length > 10 || username.Length < 4)
            {
                validationErrors.Add(new CustomErrorType("The username must be between 4 and 10 characters long.", Severity.WARNING));
            }

            /* Verifying that the username contains only letters */
            if (!Regex.IsMatch(username, @"^[a-zA-Z]+$"))
            {
                validationErrors.Add(new CustomErrorType("The username must only contain letters (a-z, A-Z).", Severity.ERROR));
            }

            return validationErrors.Count == 0;
        }
    }
}
