using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.Interfaces
{
    /// <summary>
    /// Interface for the DeviceModel
    /// </summary>
    public interface IDeviceModel
    {
        /// <summary>
        /// Response of the *IDN? query
        /// </summary>
        string IDN { get; }

        /// <summary>
        /// Power supply channel of the device
        /// </summary>
        IPsChannelModel PS_Channel { get; set; }

        /// <summary>
        /// DDS1 channel of the device
        /// </summary>
        IDdsChannelModel DD1_Channel { get; set; }

        /// <summary>
        /// DDS2 channel of the device
        /// </summary>
        IDdsChannelModel DD2_Channel { get; set; }
        
        /// <summary>
        /// Save the settings of the power supply
        /// </summary>
        ICommand SaveSettings { get; }

    }
}
