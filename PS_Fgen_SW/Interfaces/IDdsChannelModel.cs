using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Enumerations;

namespace PS_Fgen_SW.Interfaces
{
    /// <summary>
    /// Interface for the DdsChannelModel
    /// </summary>
    public interface IDdsChannelModel
    {
        /// <summary>
        /// Index of the channel.
        /// </summary>
        int ChannelNumber { get; set; }

        /// <summary>
        /// Is the channel enabled or not. If enabled, the signal is available at the output.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Frequency of the DDS channel.
        /// </summary>
        float Frequency { get; set; }

        /// <summary>
        /// Amplitude of the DDS channel.
        /// </summary>
        float Amplitude { get; set; }

        /// <summary>
        /// Offset of the DDS channel.
        /// </summary>
        float Offset { get; set; }

        /// <summary>
        /// SignalForm of the DDS channel.
        /// </summary>
        SignalForms SignalForm { get; set; }
    }
}
