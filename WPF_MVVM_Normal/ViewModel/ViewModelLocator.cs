/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPF_MVVM_Normal"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WPF_MVVM_Normal.Utility;

namespace WPF_MVVM_Normal.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    //public class ViewModelLocator
    //{
    //    / <summary>
    //    / Initializes a new instance of the ViewModelLocator class.
    //    / </summary>
    //    public ViewModelLocator()
    //    {
    //        ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

    //        //if (ViewModelBase.IsInDesignModeStatic)
    //        //{
    //        //    // Create design time view services and models
    //        //    SimpleIoc.Default.Register<IDataService, DesignDataService>();
    //        //}
    //        //else
    //        //{
    //        //    // Create run time view services and models
    //        //    SimpleIoc.Default.Register<IDataService, DataService>();
    //        //}

    //        SimpleIoc.Default.Register<MainViewModel>();
    //    }

    //    public MainViewModel Main
    //    {
    //        get
    //        {
    //            return ServiceLocator.Current.GetInstance<MainViewModel>();
    //        }
    //    }

    //    public static void Cleanup()
    //    {
    //         TODO Clear the ViewModels
    //    }
    //}






    //不使用上面缺省实现，因为太依赖ViewModel类型，比如MainViewModel，不易扩展
    public class ViewModelLocator
    {
        public object Song
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("歌曲");
            }
        }

        public object Second
        {
            get
            {
                return PluginCatalogService.Instance.GetPlugin<IPluginViewModel>("新窗口");
            }
        }
    }
}