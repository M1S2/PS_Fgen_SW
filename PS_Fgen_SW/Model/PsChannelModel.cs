using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Communication;

namespace PS_Fgen_SW.Model
{
    /// <summary>
    /// Class representing a power supply channel of the PS_Fgen device.
    /// </summary>
    public class PsChannelModel : ObservableObject
    {
        private NumberFormatInfo nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Interface used to communicate to the device
        /// </summary>
        public Comm CommIF { get; set; }

        private float _channelNumber;
        public float ChannelNumber
        {
            get => _channelNumber;
            set { Set(ref _channelNumber, value); }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _enabled;
        /// <summary>
        /// Is the channel enabled or not. If enabled, the voltage is available at the output.
        /// </summary>
        public bool Enabled
        {
            get
            {
                CommIF?.Write($"OUTP{ChannelNumber}?");
                string readString = CommIF?.ReadLine() ?? "0";
                _enabled = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _enabled;
            }
            set
            {
                if(_enabled != value)
                {
                    _enabled = value;
                    CommIF?.Write($"OUTP{ChannelNumber} " + (_enabled ? "1" : "0"));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _voltage;
        /// <summary>
        /// Voltage of the power supply channel. This is the voltage that the PID regulator tries to produce on the output in CV state.
        /// </summary>
        public float Voltage
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT?");
                _voltage = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _voltage;
            }
            set 
            { 
                if(_voltage != value)
                {
                    _voltage = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT " + _voltage.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _current;
        /// <summary>
        /// Current of the power supply channel. This is the current that the PID regulator tries to produce on the output in CC state.
        /// </summary>
        public float Current
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:CURR?");
                _current = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _current;
            }
            set
            {
                if (_current != value)
                {
                    _current = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:CURR " + _current.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _measuredVoltage;
        /// <summary>
        /// Measured Voltage for this channel. This value is used for PID regulation of the output voltage in CV state.
        /// </summary>
        public float MeasuredVoltage
        {
            get
            {
                CommIF?.Write($"MEAS{ChannelNumber}:VOLT?");
                _measuredVoltage = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _measuredVoltage;
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _measuredCurrent;
        /// <summary>
        /// Measured Current for this channel. This value is used for PID regulation of the output current in CC state.
        /// </summary>
        public float MeasuredCurrent
        {
            get
            {
                CommIF?.Write($"MEAS{ChannelNumber}:CURR?");
                _measuredCurrent = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _measuredCurrent;
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _ovpState;
        /// <summary>
        /// Is the over voltage protection for the channel enabled or not. If disabled, the OvpLevel and OvpDelay parameters have not effect.
        /// </summary>
        public bool OvpState
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:STAT?");
                string readString = CommIF?.ReadLine() ?? "0";
                _ovpState = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _ovpState;
            }
            set
            {
                if (_ovpState != value)
                {
                    _ovpState = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:STAT " + (_ovpState ? "1" : "0"));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private byte _ovpLevel;
        /// <summary>
        /// OVP trip level in percentage of the Voltage.
        /// </summary>
        public byte OvpLevel
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT?");
                _ovpLevel = byte.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _ovpLevel;
            }
            set
            {
                if (_ovpLevel != value)
                {
                    _ovpLevel = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT " + _ovpLevel.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _ovpDelay;
        /// <summary>
        /// Time after which the over voltage protection kicks in.
        /// </summary>
        public float OvpDelay
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:DEL?");
                _ovpDelay = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _ovpDelay;
            }
            set
            {
                if (_ovpDelay != value)
                {
                    _ovpDelay = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:DEL " + _ovpDelay.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _ovpTripped;
        /// <summary>
        /// Was the over voltage protection tripped?
        /// </summary>
        public bool OvpTripped
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:TRIP?");
                string readString = CommIF?.ReadLine() ?? "0";
                _ovpTripped = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _ovpTripped;
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _ocpState;
        /// <summary>
        /// Is the over current protection for the channel enabled or not. If disabled, the OcpLevel and OcpDelay parameters have not effect.
        /// </summary>
        public bool OcpState
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:STAT?");
                string readString = CommIF?.ReadLine() ?? "0";
                _ocpState = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _ocpState;
            }
            set
            {
                if (_ocpState != value)
                {
                    _ocpState = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:STAT " + (_ocpState ? "1" : "0"));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private byte _ocpLevel;
        /// <summary>
        /// OCP trip level in percentage of the Current.
        /// </summary>
        public byte OcpLevel
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT?");
                _ocpLevel = byte.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _ocpLevel;
            }
            set
            {
                if (_ocpLevel != value)
                {
                    _ocpLevel = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT " + _ocpLevel.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _ocpDelay;
        /// <summary>
        /// Time after which the over current protection kicks in.
        /// </summary>
        public float OcpDelay
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:DEL?");
                _ocpDelay = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _ocpDelay;
            }
            set
            {
                if (_ocpDelay != value)
                {
                    _ocpDelay = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:DEL " + _ocpDelay.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _ocpTripped;
        /// <summary>
        /// Was the over current protection tripped?
        /// </summary>
        public bool OcpTripped
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:TRIP?");
                string readString = CommIF?.ReadLine() ?? "0";
                _ocpTripped = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _ocpTripped;
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _oppState;
        /// <summary>
        /// Is the over power protection for the channel enabled or not. If disabled, the OppLevel and OppDelay parameters have not effect.
        /// </summary>
        public bool OppState
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:STAT?");
                string readString = CommIF?.ReadLine() ?? "0";
                _oppState = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _oppState;
            }
            set
            {
                if (_oppState != value)
                {
                    _oppState = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:STAT " + (_oppState ? "1" : "0"));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _oppLevel;
        /// <summary>
        /// OPP trip level in Watt.
        /// </summary>
        public float OppLevel
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT?");
                _oppLevel = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _oppLevel;
            }
            set
            {
                if (_oppLevel != value)
                {
                    _oppLevel = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT " + _oppLevel.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _oppDelay;
        /// <summary>
        /// Time after which the over power protection kicks in.
        /// </summary>
        public float OppDelay
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:DEL?");
                _oppDelay = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _oppDelay;
            }
            set
            {
                if (_oppDelay != value)
                {
                    _oppDelay = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:DEL " + _oppDelay.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _oppTripped;
        /// <summary>
        /// Was the over power protection tripped?
        /// </summary>
        public bool OppTripped
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:TRIP?");
                string readString = CommIF?.ReadLine() ?? "0";
                _oppTripped = (readString == "1" || readString.ToUpper() == "ON") ? true : false;
                return _oppTripped;
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Clear the protections for the power supply channel and return to the CV state.
        /// </summary>
        public ICommand ClearProtections => new RelayCommand(() =>
        {
            CommIF?.Write($"SOUR{ChannelNumber}:VOLT:PROT:CLE");
            string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data

            CommIF?.Write($"SOUR{ChannelNumber}:CURR:PROT:CLE");
            dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data

            CommIF?.Write($"SOUR{ChannelNumber}:POW:PROT:CLE");
            dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
        });

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Constructor for the power supply channel.
        /// </summary>
        /// <param name="channelNumber">Channel index. This is the number used in the SCPI commands for channel identification</param>
        /// <param name="commIF">Communication interface used for communication to the device</param>
        public PsChannelModel(int channelNumber, Comm commIF)
        {
            CommIF = commIF;
            ChannelNumber = channelNumber;
        }
    }
}
