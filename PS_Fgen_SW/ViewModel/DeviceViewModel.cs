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
using GalaSoft.MvvmLight.Command;
using System.Globalization;
using System.Windows.Threading;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// Device View model
    /// </summary>
    public class DeviceViewModel : ViewModelBase
    {
        private IDeviceModel _device;
        /// <summary>
        /// Device Model of the PS_Fgen device.
        /// </summary>
        public IDeviceModel Device
        {
            get => _device;
            set { Set(ref _device, value); }
        }

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
        /// Constructor for the DeviceViewModel.
        /// </summary>
        /// <param name="commIF">Communication interface</param>
        /// <param name="deviceModel">Device model interface</param>
        public DeviceViewModel(IComm commIF, IDeviceModel deviceModel)
        {
            Comm = commIF;
            Device = deviceModel;
        }

    }
}
