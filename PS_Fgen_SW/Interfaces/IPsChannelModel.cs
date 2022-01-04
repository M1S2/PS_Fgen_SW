using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PS_Fgen_SW.Interfaces
{
    /// <summary>
    /// Interface for the PsChannelModel
    /// </summary>
    public interface IPsChannelModel
    {
        /// <summary>
        /// Index of the channel.
        /// </summary>
        int ChannelNumber { get; set; }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Is the channel enabled or not. If enabled, the voltage is available at the output.
        /// </summary>
        bool Enabled { get; set; }
        
        /// <summary>
        /// Voltage of the power supply channel. This is the voltage that the PID regulator tries to produce on the output in CV state.
        /// </summary>
        float Voltage { get; set; }
       
        /// <summary>
        /// Current of the power supply channel. This is the current that the PID regulator tries to produce on the output in CC state.
        /// </summary>
        float Current { get; set; }

        /// <summary>
        /// Measured Voltage for this channel. This value is used for PID regulation of the output voltage in CV state.
        /// </summary>
        float MeasuredVoltage { get; }

        /// <summary>
        /// Measured Current for this channel. This value is used for PID regulation of the output current in CC state.
        /// </summary>
        float MeasuredCurrent { get; }
        
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            /// <summary>
        /// Is the over voltage protection for the channel enabled or not. If disabled, the OvpLevel and OvpDelay parameters have not effect.
        /// </summary>
        bool OvpState { get; set; }
        
        /// <summary>
        /// OVP trip level in percentage of the Voltage.
        /// </summary>
        byte OvpLevel { get; set; }

        /// <summary>
        /// Time after which the over voltage protection kicks in.
        /// </summary>
        float OvpDelay { get; set; }

        /// <summary>
        /// Was the over voltage protection tripped?
        /// </summary>
        bool OvpTripped { get; }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            /// <summary>
        /// Is the over current protection for the channel enabled or not. If disabled, the OcpLevel and OcpDelay parameters have not effect.
        /// </summary>
        bool OcpState { get; set; }

        /// <summary>
        /// OCP trip level in percentage of the Current.
        /// </summary>
        byte OcpLevel { get; set; }

        /// <summary>
        /// Time after which the over current protection kicks in.
        /// </summary>
        float OcpDelay { get; set; }
     
        /// <summary>
        /// Was the over current protection tripped?
        /// </summary>
        bool OcpTripped { get; }
       
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Is the over power protection for the channel enabled or not. If disabled, the OppLevel and OppDelay parameters have not effect.
        /// </summary>
        bool OppState { get; set; }

        /// <summary>
        /// OPP trip level in Watt.
        /// </summary>
        float OppLevel { get; set; }

        /// <summary>
        /// Time after which the over power protection kicks in.
        /// </summary>
        float OppDelay { get; set; }

        /// <summary>
        /// Was the over power protection tripped?
        /// </summary>
        bool OppTripped { get; }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Clear the protections for the power supply channel and return to the CV state.
        /// </summary>
        ICommand ClearProtections { get; }

        /// <summary>
        /// Update all measured parameters.
        /// </summary>
        ICommand UpdateMeasuredParameters { get; }
    }
}
