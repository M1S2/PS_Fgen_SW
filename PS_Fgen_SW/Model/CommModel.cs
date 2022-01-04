using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Interfaces;

namespace PS_Fgen_SW.Model
{
    /// <summary>
    /// Class representing a communication model
    /// </summary>
    public class CommModel : ObservableObject, IComm
    {
        private bool _connected;
        /// <summary>
        /// Is the communication channel connected or not
        /// </summary>
        public bool Connected
        {
            get => _connected;
            set { Set(ref _connected, value); }
        }


        private IComm _commHandle;

        public CommModel()
        {
            Connected = false;
            _commHandle = new Communication.CommSim();
            //_commHandle = new Communication.CommSerial("COM10");
        }

        /// <summary>
        /// Connect to the communication interface
        /// </summary>
        public void Connect()
        {
            _commHandle.Connect();
            Connected = true;
        }

        /// <summary>
        /// Disconnect from the communication interface
        /// </summary>
        public void Disconnect()
        {
            _commHandle.Disconnect();
            Connected = false;
        }

        /// <summary>
        /// Write to the communication interface
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, is appended automatically)</param>
        public void Write(string message)
        {
            _commHandle.Write(message);
        }

        /// <summary>
        /// Read one line from the communication interface (until the LineEnding is reached)
        /// </summary>
        /// <returns>Read string</returns>
        public string ReadLine()
        {
            return _commHandle.ReadLine();
        }
    }
}
