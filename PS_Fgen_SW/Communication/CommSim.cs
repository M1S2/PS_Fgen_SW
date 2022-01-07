using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PS_Fgen_SW.Interfaces;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Class used to simulate a communication interface. This can be used for testing without a device.
    /// </summary>
    public class CommSim : ObservableObject, IComm
    {
        /// <summary>
        /// Line Ending used when writing
        /// </summary>
        public static string LineEnding = "\r\n";

        private SimulationDevice _simDevice;            // Device used for simulation
        private string _simDeviceResponse;              // Response from the ProcessData method of the simulation device
        private string _lastCommand;                    // Last command string that was written over the communication interface

        private bool _connected;
        /// <summary>
        /// Is the communication channel connected or not
        /// </summary>
        public bool Connected
        {
            get => _connected;
            set => Set(ref _connected, value);
        }

        /// <summary>
        /// Constructor of the simulation interface
        /// <param name="commEcho">Echo all received data back</param>
        /// </summary>
        public CommSim(bool commEcho = false)
        {
            _simDevice = new SimulationDevice(commEcho);
        }

        /// <summary>
        /// Connect to the simulation interface (do nothing)
        /// </summary>
        public void Connect()
        {
            Connected = true;
        }

        /// <summary>
        /// Disconnect from the simulation interface (do nothing)
        /// </summary>
        public void Disconnect()
        {
            Connected = false;
        }

        /// <summary>
        /// Write to the simlation interface
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, isn't needed here)</param>
        public void Write(string message)
        {
            if (Connected)
            {
                try
                {
                    string[] messageParts = message.Split(' ');
                    string command = messageParts.FirstOrDefault();
                    _lastCommand = command;

                    _simDeviceResponse = _simDevice.ProcessData(message + LineEnding);
                }
                catch (Exception)
                {
                    Connected = false;
                }
            }
        }

        /// <summary>
        /// Read one line from the simulation interface
        /// </summary>
        /// <returns>Read string</returns>
        public string ReadLine()
        {
            if (Connected)
            {
                try
                {
                    string readString = _simDeviceResponse;
                    return readString.Replace(_lastCommand, "").Trim(LineEnding.ToCharArray()).Trim(' ');
                }
                catch (Exception)
                {
                    Connected = false;
                }
            }
            return "0";
        }
    }
}
