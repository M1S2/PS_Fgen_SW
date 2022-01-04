using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Interfaces;

namespace PS_Fgen_SW.Model
{
    /// <summary>
    /// Class representing the PS_Fgen device.
    /// </summary>
    public class DeviceModel : ObservableObject, IDeviceModel
    {
        /// <summary>
        /// Interface used to communicate to the device
        /// </summary>
        public IComm CommIF { get; set; }

        /// <summary>
        /// Response of the *IDN? query
        /// </summary>
        public string IDN { get; private set; }

        private IPsChannelModel _ps_Channel;
        /// <summary>
        /// Power supply channel of the device
        /// </summary>
        public IPsChannelModel PS_Channel
        {
            get => _ps_Channel;
            set { Set(ref _ps_Channel, value); }
        }

        private IDdsChannelModel _dds1_Channel;
        /// <summary>
        /// DDS1 channel of the device
        /// </summary>
        public IDdsChannelModel DD1_Channel
        {
            get => _dds1_Channel;
            set { Set(ref _dds1_Channel, value); }
        }

        private IDdsChannelModel _dds2_Channel;
        /// <summary>
        /// DDS2 channel of the device
        /// </summary>
        public IDdsChannelModel DD2_Channel
        {
            get => _dds2_Channel;
            set { Set(ref _dds2_Channel, value); }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Save the settings of the power supply
        /// </summary>
        public ICommand SaveSettings => new RelayCommand(() =>
        {
            CommIF?.Write("*SAV");
            string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
        });

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public DeviceModel(IComm commIF)
        {
            CommIF = commIF;
            PS_Channel = new PsChannelModel(0, commIF);
            DD1_Channel = new DdsChannelModel(1, commIF);
            DD2_Channel = new DdsChannelModel(2, commIF);

            CommIF?.Write("*IDN?");
            IDN = CommIF?.ReadLine() ?? "";
        }
    }
}
