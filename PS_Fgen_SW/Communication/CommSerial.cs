using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using PS_Fgen_SW.Interfaces;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Class used to communicate via the serial port
    /// </summary>
    public class CommSerial : IComm
    {
        /// <summary>
        /// Line Ending used when writing
        /// </summary>
        public static string LineEnding = "\r\n";

        private SerialPort _comPort;            // Serial port used for communication
        private string _lastCommand;                    // Last command string that was written over the communication interface

        /// <summary>
        /// Is the communication channel connected or not
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// Constructor of the serial communication port
        /// </summary>
        /// <param name="portName">Port name to use (e.g. COM10)</param>
        /// <param name="baudRate">Baud rate for communication (e.g. 9600)</param>
        /// <param name="parity">Parity for communication (e.g. Parity.None)</param>
        /// <param name="dataBits">Number of data bits for the communication (e.g. 8)</param>
        /// <param name="stopBits">Number of stop bits for the communication (e.g. StopBits.One)</param>
        public CommSerial(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.None)
        {
            _comPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            _comPort.NewLine = LineEnding;
        }

        /// <summary>
        /// Connect to the serial communication port
        /// </summary>
        public void Connect()
        {
            _comPort?.Open();
            Connected = true;
        }

        /// <summary>
        /// Disconnect from the serial communication port
        /// </summary>
        public void Disconnect()
        {
            _comPort?.Close();
            Connected = false;
        }

        /// <summary>
        /// Write to the serial communication port
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, is appended automatically)</param>
        public void Write(string message)
        {
            if (Connected)
            {
                string[] messageParts = message.Split(' ');
                string command = messageParts.FirstOrDefault();
                _lastCommand = command;

                _comPort.Write(message + LineEnding);
            }
        }

        /// <summary>
        /// Read one line from the serial communication port (until the LineEnding is reached).
        /// This method removed the previously written command from the response (needed if serial echo is enabled).
        /// </summary>
        /// <returns>Read string</returns>
        public string ReadLine()
        {
            if (Connected)
            {
                string readString = _comPort.ReadLine();
                return readString.Replace(_lastCommand, "").Trim(LineEnding.ToCharArray()).Trim(' ');
            }
            return "";
        }
    }
}
