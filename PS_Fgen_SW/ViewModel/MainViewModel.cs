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
        /// Command executed when the window is closing.
        /// </summary>
        public ICommand WindowClosing => new RelayCommand<CancelEventArgs>
        (
            (args) =>
            {
                Comm.Disconnect();
            }
        );

        /// <summary>
        /// Constructor for the MainViewModel.
        /// </summary>
        /// <param name="commIF">Communication interface</param>
        public MainViewModel(IComm commIF)
        {
            Comm = commIF;
        }

    }
}
