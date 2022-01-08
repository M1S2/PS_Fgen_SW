using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.Interfaces;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Globalization;
using System.Windows.Threading;
using System.Windows;
using CommonServiceLocator;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// Main View model. Holding e.g. the DeviceModel and the Comm handle.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Command to show the communication view
        /// </summary>
        public ICommand ShowCommViewCommand => new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.CommView));

        /// <summary>
        /// Command to show the device view
        /// </summary>
        public ICommand ShowDeviceViewCommand => new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.DeviceView), () => Comm.Connected);

        private IComm _comm;
        /// <summary>
        /// Communication interface used for communication to the PS_Fgen device.
        /// </summary>
        public IComm Comm
        {
            get => _comm;
            set { Set(ref _comm, value); }
        }

        /// <summary>
        /// Command executed when the window is loaded.
        /// </summary>
        public ICommand WindowLoaded => new RelayCommand<RoutedEventArgs>
        (
            (args) =>
            {
                _navigationService.NavigateTo(ViewModelLocator.CommView);
            }
        );

        /// <summary>
        /// Command executed when the window is closing.
        /// </summary>
        public ICommand WindowClosing => new RelayCommand<CancelEventArgs>
        (
            (args) =>
            {
                Comm.Disconnect();
            }
        );

        private IFrameNavigationService _navigationService;

        /// <summary>
        /// Constructor for the MainViewModel.
        /// </summary>
        /// <param name="commIF">Communication interface</param>
        /// <param name="navigationService">Navigation service</param>
        public MainViewModel(IComm commIF, IFrameNavigationService navigationService)
        {
            Comm = commIF;
            _navigationService = navigationService;
            ((ObservableObject)Comm).PropertyChanged += Comm_OnConnectionStateChanged;
        }

        /// <summary>
        /// The connection state of the communication interface changed.
        /// </summary>
        private void Comm_OnConnectionStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IComm.Connected))
            {
                if (Comm.Connected == true)
                {
                    _navigationService.NavigateTo(ViewModelLocator.DeviceView);
                }
                else
                {
                    _navigationService.NavigateTo(ViewModelLocator.CommView);
                }
            }
        }
    }
}
