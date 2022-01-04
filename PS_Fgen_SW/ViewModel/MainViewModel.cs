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
    /// Main View model. Holding e.g. the DeviceModel and the Comm handle.
    /// </summary>
    public class MainViewModel : ViewModelBase
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

        private string _statusStr;
        /// <summary>
        /// Status string indicating the current operation of the GUI.
        /// </summary>
        public string StatusStr
        {
            get => _statusStr;
            set { Set(ref _statusStr, value); }
        }

        /// <summary>
        /// Command for writing some test parameters to the PS_Fgen device.
        /// </summary>
        public ICommand WriteToDeviceCommand => new RelayCommand
        (
            async () => await WriteToDevice()
        );

        /// <summary>
        /// Command executed when the window is closing.
        /// </summary>
        public ICommand WindowClosing => new RelayCommand<CancelEventArgs>
        (
            (args) =>
            {
                _measureTimer.Stop();
                Comm.Disconnect();
            }
        );

        /// <summary>
        /// Constructor for the MainViewModel.
        /// </summary>
        /// <param name="commIF">Communication interface</param>
        /// <param name="deviceModel">Device model interface</param>
        public MainViewModel(IComm commIF, IDeviceModel deviceModel)
        {
            Comm = commIF;
            Device = deviceModel;
            StatusStr = "Ready";

            _measureTimer.Interval = new TimeSpan(0, 0, 1);
            _measureTimer.Tick += _measureTimer_Tick;
            _measureTimer.Start();
        }

        /// <summary>
        /// Get some parameters periodically from the device.
        /// </summary>
        private void _measureTimer_Tick(object sender, EventArgs e)
        {
            StatusStr = "Update GUI...";
            Device?.PS_Channel?.UpdateMeasuredParameters?.Execute(null);
            StatusStr = "Ready";
        }

        /// <summary>
        /// Write some test parameters to the PS_Fgen device.
        /// </summary>
        private async Task WriteToDevice()
        {
            await Task.Run(() =>
            {
                StatusStr = "Write to device...";
                Device.PS_Channel.Voltage = 2;
                Device.PS_Channel.Current = 0.9f;
                Device.PS_Channel.Enabled = true;
                StatusStr = "Ready";
            });
        }

    }
}
