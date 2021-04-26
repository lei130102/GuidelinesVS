using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//INotifyDataErrorInfo

//The.NET Framework 4.5 introduced a new System.ComponentModel.INotifyDataErrorInfo interface – the same interface
//has been present in Silverlight since version 4 – which enables you to perform server-side validations asynchronously
//and then notify the view by raising an ErrorsChanged event once the validations are completed. Similarly, it makes
//it possible to invalidate a property when setting another property and it also supports setting multiple errors per
//property and custom error objects of some other type than System.String (string).

//虽然从.NET3.5开始出现的IDataErrorInfo接口基本上只提供返回指定单个给定属性错误的字符串的功能，但新的INotifyDataErrorInfo接口提供了更大的灵活性，通常应在实现新类时使用。

namespace WPF_Validation_INotifyDataErrorInfo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
