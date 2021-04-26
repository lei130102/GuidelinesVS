using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_MVVM_Normal
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //先初始化，再调用DispatcherHelper.CheckBeginInvokeOnUI方法来实现对UI线程的调度
            DispatcherHelper.Initialize();
        }
    }
}
