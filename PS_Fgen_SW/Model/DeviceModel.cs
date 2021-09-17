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

        private PsChannelModel _ps_Channel;
        /// <summary>
        /// Power supply channel of the device
        /// </summary>
        public PsChannelModel PS_Channel
        {
            get => _ps_Channel;
            set { Set(ref _ps_Channel, value); }
        }

        public DeviceModel(Comm commIF)
        {
            CommIF = commIF;
            PS_Channel = new PsChannelModel(0, commIF);
        }
    }
}
