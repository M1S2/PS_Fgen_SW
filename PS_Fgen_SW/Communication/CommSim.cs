using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS_Fgen_SW.Model;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Class used to simulate a communication interface. This can be used for testing without a device.
    /// </summary>
    public class CommSim : Comm
    {
        private SimulationDevice _simDevice;            // Device used for simulation
        private string _simDeviceResponse;              // Response from the ProcessData method of the simulation device

        /// <summary>
        /// Constructor of the simulation interface
        /// <param name="commEcho">Echo all received data back</param>
        /// </summary>
        public CommSim(bool commEcho)
        {
            _simDevice = new SimulationDevice(commEcho);
        }

        /// <summary>
        /// Open the simulation interface (do nothing)
        /// </summary>
        public override void Open() { }

        /// <summary>
        /// Close the simulation interface (do nothing)
        /// </summary>
        public override void Close() { }

        /// <summary>
        /// Write to the simlation interface
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, isn't needed here)</param>
        public override void Write(string message)
        {
            string[] messageParts = message.Split(' ');
            string command = messageParts.FirstOrDefault();
            _lastCommand = command;

            _simDeviceResponse = _simDevice.ProcessData(message + LineEnding);
        }

        /// <summary>
        /// Read one line from the simulation interface
        /// </summary>
        /// <returns>Read string</returns>
        public override string ReadLine()
        {
            string readString = _simDeviceResponse;
            return readString.Replace(_lastCommand, "").Trim(LineEnding.ToCharArray()).Trim(' ');
        }
    }
}
