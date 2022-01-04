using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PS_Fgen_SW.Interfaces;
using PS_Fgen_SW.Enumerations;

namespace PS_Fgen_SW.Model
{
    /// <summary>
    /// Class representing a direct digital synthesis channel of the PS_Fgen device.
    /// </summary>
    public class DdsChannelModel : ObservableObject, IDdsChannelModel
    {
        private NumberFormatInfo nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Interface used to communicate to the device
        /// </summary>
        public IComm CommIF { get; set; }

        private int _channelNumber;
        /// <summary>
        /// Index of the channel.
        /// </summary>
        public int ChannelNumber
        {
            get => _channelNumber;
            set { Set(ref _channelNumber, value); }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private bool _enabled;
        /// <summary>
        /// Is the channel enabled or not. If enabled, the signal is available at the output.
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

        private float _frequency;
        /// <summary>
        /// Frequency of the DDS channel.
        /// </summary>
        public float Frequency
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:FREQ?");
                _frequency = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _frequency;
            }
            set 
            { 
                if(_frequency != value)
                {
                    _frequency = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:FREQ " + _frequency.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _amplitude;
        /// <summary>
        /// Amplitude of the DDS channel.
        /// </summary>
        public float Amplitude
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT?");
                _amplitude = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _amplitude;
            }
            set
            {
                if (_amplitude != value)
                {
                    _amplitude = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT " + _amplitude.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private float _offset;
        /// <summary>
        /// Offset of the DDS channel.
        /// </summary>
        public float Offset
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:VOLT:OFFS?");
                _offset = float.Parse(CommIF?.ReadLine() ?? "0", NumberStyles.Float, CultureInfo.InvariantCulture);
                return _offset;
            }
            set
            {
                if (_offset != value)
                {
                    _offset = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:VOLT:OFFS " + _offset.ToString(nfi));
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private SignalForms _signalForm;
        /// <summary>
        /// SignalForm of the DDS channel.
        /// </summary>
        public SignalForms SignalForm
        {
            get
            {
                CommIF?.Write($"SOUR{ChannelNumber}:FUNC?");
                string signalFormStr = CommIF?.ReadLine() ?? SignalForms.SINusoid.ToString();
                if(string.IsNullOrEmpty(signalFormStr)) { return SignalForms.SINusoid; }
                _signalForm = (SignalForms)Enum.Parse(typeof(SignalForms), signalFormStr);
                return _signalForm;
            }
            set
            {
                if (_signalForm != value)
                {
                    _signalForm = value;
                    CommIF?.Write($"SOUR{ChannelNumber}:FUNC " + _signalForm.ToString());
                    string dummyRead = CommIF?.ReadLine();     // Dummy read to get echoed data
                    RaisePropertyChanged();
                }
            }
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Constructor for the DDS channel.
        /// </summary>
        /// <param name="channelNumber">Channel index. This is the number used in the SCPI commands for channel identification</param>
        /// <param name="commIF">Communication interface used for communication to the device</param>
        public DdsChannelModel(int channelNumber, IComm commIF)
        {
            CommIF = commIF;
            ChannelNumber = channelNumber;
        }
    }
}
