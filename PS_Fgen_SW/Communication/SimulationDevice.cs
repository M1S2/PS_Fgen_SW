using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Simulation for a PS_Fgen device
    /// </summary>
    public class SimulationDevice
    {
        public const string ScientificNumberFormat = "E6";

        private bool _commEcho;                     // Echo all received data back

        private bool _psChannel_Enabled;
        private float _psChannel_Voltage;
        private float _psChannel_Current;
        private float _psChannel_MeasuredVolt;
        private float _psChannel_MeasuredCurrent;
        private bool _psChannel_OvpState;
        private byte _psChannel_OvpLevel;
        private float _psChannel_OvpDelay;
        private bool _psChannel_OvpTripped;
        private bool _psChannel_OcpState;
        private byte _psChannel_OcpLevel;
        private float _psChannel_OcpDelay;
        private bool _psChannel_OcpTripped;
        private bool _psChannel_OppState;
        private float _psChannel_OppLevel;
        private float _psChannel_OppDelay;
        private bool _psChannel_OppTripped;

        /// <summary>
        /// Constructor of the simulation device
        /// </summary>
        /// <param name="commEcho">Echo all received data back</param>
        public SimulationDevice(bool commEcho)
        {
            _commEcho = commEcho;
        }

        /// <summary>
        /// Simulates receiving data via the communication port, processing it and eventually sending back a response
        /// </summary>
        /// <param name="message">input message string with <see cref="Comm.LineEnding"/></param>
        /// <returns>Eventually response string (depending on the message)</returns>
        public string ProcessData(string message)
        {
            string returnVal = _commEcho ? message : "";
            string[] messageParts = message.Trim(Comm.LineEnding.ToCharArray()).Split(' ');
            string command = messageParts.FirstOrDefault();
            Random rand = new Random();

            //System.Threading.Thread.Sleep(100);

            try
            {
                switch (command.ToUpper())
                {
                    case "SYST:REM":
                        // Nothing to return
                        break;
                    /*******************************************************************************************************************/
                    case "OUTP0":
                        _psChannel_Enabled = (messageParts[1] == "1" || messageParts[1].ToUpper() == "ON") ? true : false;
                        // Nothing to return
                        break;
                    case "OUTP0?":
                        returnVal = _psChannel_Enabled ? "1" : "0";
                        break;
                    case "SOUR0:VOLT":
                        _psChannel_Voltage = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:VOLT?":
                        returnVal = _psChannel_Voltage.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:CURR":
                        _psChannel_Current = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:CURR?":
                        returnVal = _psChannel_Current.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "MEAS0:VOLT?":
                        _psChannel_MeasuredVolt = _psChannel_Enabled ? (_psChannel_Voltage + (0.2f * ((float)rand.NextDouble() - 0.5f))) : 0;

                        if (_psChannel_OvpState && _psChannel_MeasuredVolt > (_psChannel_Voltage * (_psChannel_OvpLevel / 100.0f)))
                        {
                            _psChannel_OvpTripped = true;
                        }
                        else if (_psChannel_OppState && (_psChannel_MeasuredVolt * _psChannel_MeasuredCurrent) > _psChannel_OppLevel)
                        {
                            _psChannel_OppTripped = true;
                        }
                        if (_psChannel_OvpTripped || _psChannel_OcpTripped || _psChannel_OppTripped) { _psChannel_MeasuredVolt = 0; }
                        returnVal = _psChannel_MeasuredVolt.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "MEAS0:CURR?":
                        _psChannel_MeasuredCurrent = _psChannel_Enabled ? (_psChannel_Current + (0.2f * ((float)rand.NextDouble() - 0.5f))) : 0;
                        
                        if (_psChannel_OcpState && _psChannel_MeasuredCurrent > (_psChannel_Current * (_psChannel_OcpLevel / 100.0f)))
                        {
                            _psChannel_OcpTripped = true; 
                        }
                        else if (_psChannel_OppState && (_psChannel_MeasuredVolt * _psChannel_MeasuredCurrent) > _psChannel_OppLevel)
                        {
                            _psChannel_OppTripped = true;
                        }
                        if (_psChannel_OvpTripped || _psChannel_OcpTripped || _psChannel_OppTripped) { _psChannel_MeasuredCurrent = 0; }
                        returnVal = _psChannel_MeasuredCurrent.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    /*-----------------------------------------------------------------------------------------------------------------*/
                    case "SOUR0:VOLT:PROT:STAT":
                        _psChannel_OvpState = (messageParts[1] == "1" || messageParts[1].ToUpper() == "ON") ? true : false;
                        // Nothing to return
                        break;
                    case "SOUR0:VOLT:PROT:STAT?":
                        returnVal = _psChannel_OvpState ? "1" : "0";
                        break;
                    case "SOUR0:VOLT:PROT":
                        _psChannel_OvpLevel = byte.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:VOLT:PROT?":
                        returnVal = _psChannel_OvpLevel.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:VOLT:PROT:DEL":
                        _psChannel_OvpDelay = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:VOLT:PROT:DEL?":
                        returnVal = _psChannel_OvpDelay.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:VOLT:PROT:TRIP?":
                        returnVal = _psChannel_OvpTripped ? "1" : "0";
                        break;
                    case "SOUR0:VOLT:PROT:CLE":
                        _psChannel_OvpTripped = false;
                        break;
                    /*-----------------------------------------------------------------------------------------------------------------*/
                    case "SOUR0:CURR:PROT:STAT":
                        _psChannel_OcpState = (messageParts[1] == "1" || messageParts[1].ToUpper() == "ON") ? true : false;
                        // Nothing to return
                        break;
                    case "SOUR0:CURR:PROT:STAT?":
                        returnVal = _psChannel_OcpState ? "1" : "0";
                        break;
                    case "SOUR0:CURR:PROT":
                        _psChannel_OcpLevel = byte.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:CURR:PROT?":
                        returnVal = _psChannel_OcpLevel.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:CURR:PROT:DEL":
                        _psChannel_OcpDelay = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:CURR:PROT:DEL?":
                        returnVal = _psChannel_OcpDelay.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:CURR:PROT:TRIP?":
                        returnVal = _psChannel_OcpTripped ? "1" : "0";
                        break;
                    case "SOUR0:CURR:PROT:CLE":
                        _psChannel_OcpTripped = false;
                        break;
                    /*-----------------------------------------------------------------------------------------------------------------*/
                    case "SOUR0:POW:PROT:STAT":
                        _psChannel_OppState = (messageParts[1] == "1" || messageParts[1].ToUpper() == "ON") ? true : false;
                        // Nothing to return
                        break;
                    case "SOUR0:POW:PROT:STAT?":
                        returnVal = _psChannel_OppState ? "1" : "0";
                        break;
                    case "SOUR0:POW:PROT":
                        _psChannel_OppLevel = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:POW:PROT?":
                        returnVal = _psChannel_OppLevel.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:POW:PROT:DEL":
                        _psChannel_OppDelay = float.Parse(messageParts[1]);
                        // Nothing to return
                        break;
                    case "SOUR0:POW:PROT:DEL?":
                        returnVal = _psChannel_OppDelay.ToString(ScientificNumberFormat, CultureInfo.InvariantCulture);
                        break;
                    case "SOUR0:POW:PROT:TRIP?":
                        returnVal = _psChannel_OppTripped ? "1" : "0";
                        break;
                    case "SOUR0:POW:PROT:CLE":
                        _psChannel_OppTripped = false;
                        break;
                    /*******************************************************************************************************************/
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnVal + Comm.LineEnding;
        }
    }
}
