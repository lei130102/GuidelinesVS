using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_Basic.Utility
{
    public interface IPluginMetadata
    {
        /// <summary>
        /// 插件对应的类型
        /// </summary>
        string[] Type { get; }
    }
}
