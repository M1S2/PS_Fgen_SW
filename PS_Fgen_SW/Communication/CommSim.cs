using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Interfaces;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Class used to simulate a communication interface. This can be used for testing without a device.
    /// </summary>
    public class CommSim : IComm
    {
        /// <summary>
        /// Line Ending used when writing
        /// </summary>
        public static string LineEnding = "\r\n";

        private SimulationDevice _simDevice;            // Device used for simulation
        private string _simDeviceResponse;              // Response from the ProcessData method of the simulation device
        private string _lastCommand;                    // Last command string that was written over the communication interface

        /// <summary>
        /// Is the communication channel connected or not
        /// </summary>
        public bool Connected { get; set; }

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
                string[] messageParts = message.Split(' ');
                string command = messageParts.FirstOrDefault();
                _lastCommand = command;

                _simDeviceResponse = _simDevice.ProcessData(message + LineEnding);
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
                string readString = _simDeviceResponse;
                return readString.Replace(_lastCommand, "").Trim(LineEnding.ToCharArray()).Trim(' ');
            }
            return "0";
        }
    }
}
