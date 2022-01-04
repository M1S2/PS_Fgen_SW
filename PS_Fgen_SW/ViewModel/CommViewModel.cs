using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Model;
using PS_Fgen_SW.Interfaces;

namespace PS_Fgen_SW.ViewModel
{
    /// <summary>
    /// View Model for the communication model
    /// </summary>
    public class CommViewModel : ViewModelBase
    {
        private IComm _commModel;
        /// <summary>
        /// Communication model interface
        /// </summary>
        public IComm CommModel
        {
            get => _commModel;
            set { Set(ref _commModel, value); }
        }

        private RelayCommand _connectDisconnectCommand;
        /// <summary>
        /// Command used to toggle between connected and disconnected
        /// </summary>
        public ICommand ConnectDisconnectCommand
        {
            get
            {
                if(_connectDisconnectCommand == null)
                {
                    _connectDisconnectCommand = new RelayCommand(() => ToggleConnectionState());
                }
                return _connectDisconnectCommand;
            }
        }

        /// <summary>
        /// Constructor of the CommViewModel
        /// </summary>
        public CommViewModel()
        {
            CommModel = ServiceLocator.Current.GetInstance<IComm>();
        }

        /// <summary>
        /// Toggle between connected and disconnected
        /// </summary>
        public void ToggleConnectionState()
        {
            if(CommModel == null) { return; }
            if(CommModel.Connected)
            {
                CommModel.Disconnect();
            }
            else
            {
                CommModel.Connect();
            }
        }

    }
}
