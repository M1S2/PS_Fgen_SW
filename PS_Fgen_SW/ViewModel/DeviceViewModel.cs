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
        DispatcherTimer _measureTimer = new DispatcherTimer();

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
            
            _measureTimer.Interval = new TimeSpan(0, 0, 1);
            _measureTimer.Tick += _measureTimer_Tick;
            _measureTimer.Start();
        }

        /// <summary>
        /// Get some parameters periodically from the device.
        /// </summary>
        private void _measureTimer_Tick(object sender, EventArgs e)
        {
            Device?.PS_Channel?.UpdateMeasuredParameters?.Execute(null);
        }

    }
}
