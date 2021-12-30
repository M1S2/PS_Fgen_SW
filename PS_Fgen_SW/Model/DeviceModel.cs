using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PS_Fgen_SW.Communication;

namespace PS_Fgen_SW.Model
{
    /// <summary>
    /// Class representing the PS_Fgen device.
    /// </summary>
    public class DeviceModel : ObservableObject
    {
        /// <summary>
        /// Interface used to communicate to the device
        /// </summary>
        public Comm CommIF { get; set; }

        /// <summary>
        /// Response of the *IDN? query
        /// </summary>
        public string IDN { get; private set; }

        private PsChannelModel _ps_Channel;
        /// <summary>
        /// Power supply channel of the device
        /// </summary>
        public PsChannelModel PS_Channel
        {
            get => _ps_Channel;
            set { Set(ref _ps_Channel, value); }
        }

        private DdsChannelModel _dds1_Channel;
        /// <summary>
        /// DDS1 channel of the device
        /// </summary>
        public DdsChannelModel DD1_Channel
        {
            get => _dds1_Channel;
            set { Set(ref _dds1_Channel, value); }
        }

        private DdsChannelModel _dds2_Channel;
        /// <summary>
        /// DDS2 channel of the device
        /// </summary>
        public DdsChannelModel DD2_Channel
        {
            get => _dds2_Channel;
            set { Set(ref _dds2_Channel, value); }
        }

        public DeviceModel(Comm commIF)
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
