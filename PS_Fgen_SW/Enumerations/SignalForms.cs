using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Fgen_SW.Enumerations
{
    /// <summary>
    /// Signal forms enumeration
    /// </summary>
    public enum SignalForms
    {
        /// <summary>
        /// Sine wave type
        /// </summary>
        SINusoid,

        /// <summary>
        /// Rectange wave type
        /// </summary>
        SQUare,

        /// <summary>
        /// Triangle wave type
        /// </summary>
        TRIangle,

        /// <summary>
        /// Sawtooth wave type
        /// </summary>
        SAWtooth,

        /// <summary>
        /// Direct current wave type
        /// </summary>
        DC,

        /// <summary>
        /// User defined wave type (signal is held in the UserWaveTable of the DDS_Channel)
        /// </summary>
        USER
    }
}
