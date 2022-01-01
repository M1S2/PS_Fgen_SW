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
using PS_Fgen_SW.Communication;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public Comm CommIF { get; set; }
        public DeviceModel DevModel { get; set; }
        public MainViewModel MainViewModelObj { get; set; }
        public DdsChannelViewModel Dds1ViewModelObj { get; set; }
        public DdsChannelViewModel Dds2ViewModelObj { get; set; }

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

            CommIF = new CommSim(false);
            //CommIF = new CommSerial("COM10", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            CommIF.Open();

            DevModel = new DeviceModel(CommIF);

            MainViewModelObj = new MainViewModel();
            MainViewModelObj.Comm = CommIF;
            MainViewModelObj.Device = DevModel;

            Dds1ViewModelObj = new DdsChannelViewModel(DevModel.DD1_Channel);
            Dds2ViewModelObj = new DdsChannelViewModel(DevModel.DD2_Channel);

            SimpleIoc.Default.Register(() => MainViewModelObj);
            SimpleIoc.Default.Register(() => Dds1ViewModelObj, "DDS1");
            SimpleIoc.Default.Register(() => Dds2ViewModelObj, "DDS2");
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DdsChannelViewModel DDS1
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DdsChannelViewModel>("DDS1");
            }
        }

        public DdsChannelViewModel DDS2
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DdsChannelViewModel>("DDS2");
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}