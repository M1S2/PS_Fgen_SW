/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PS_Fgen_SW"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using PS_Fgen_SW.Interfaces;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.Communication;
using PS_Fgen_SW.Services;
using System;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string CommView = "CommView";
        public const string DeviceView = "DeviceView";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                //SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SetupNavigation();

            //SimpleIoc.Default.Register<IComm, CommModel>();
            SimpleIoc.Default.Register<IComm>(() => new CommSim());
            SimpleIoc.Default.Register<IDeviceModel, DeviceModel>();

            SimpleIoc.Default.Register<CommViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PsChannelViewModel>();
            SimpleIoc.Default.Register(() => new DdsChannelViewModel(1), "DDS1");
            SimpleIoc.Default.Register(() => new DdsChannelViewModel(2), "DDS2");
            SimpleIoc.Default.Register<DeviceViewModel>();
        }

        public MainViewModel MainVM => ServiceLocator.Current.GetInstance<MainViewModel>();
        public PsChannelViewModel PSVM => ServiceLocator.Current.GetInstance<PsChannelViewModel>();
        public DdsChannelViewModel DDS1VM => ServiceLocator.Current.GetInstance<DdsChannelViewModel>("DDS1");
        public DdsChannelViewModel DDS2VM => ServiceLocator.Current.GetInstance<DdsChannelViewModel>("DDS2");
        public CommViewModel CommVM => ServiceLocator.Current.GetInstance<CommViewModel>();
        public DeviceViewModel DeviceVM => ServiceLocator.Current.GetInstance<DeviceViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private static void SetupNavigation()
        {
            FrameNavigationService navigationService = new FrameNavigationService();
            navigationService.Configure(CommView, new Uri("../Views/CommUserControl.xaml", UriKind.Relative));
            navigationService.Configure(DeviceView, new Uri("../Views/DeviceUserControl.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
    }
}