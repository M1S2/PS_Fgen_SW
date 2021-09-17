using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Class used to communicate via the serial port
    /// </summary>
    public class CommSerial : Comm
    { 
        private SerialPort _comPort;            // Serial port used for communication

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
        /// Open the serial communication port
        /// </summary>
        public override void Open() => _comPort?.Open();

        /// <summary>
        /// Close the serial communication port
        /// </summary>
        public override void Close() => _comPort?.Close();

        /// <summary>
        /// Write to the serial communication port
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, is appended automatically)</param>
        public override void Write(string message)
        {
            string[] messageParts = message.Split(' ');
            string command = messageParts.FirstOrDefault();
            _lastCommand = command;

            _comPort.Write(message + LineEnding);
        }

        /// <summary>
        /// Read one line from the serial communication port (until the LineEnding is reached).
        /// This method removed the previously written command from the response (needed if serial echo is enabled).
        /// </summary>
        /// <returns>Read string</returns>
        public override string ReadLine()
        {
            string readString = _comPort.ReadLine();
            return readString.Replace(_lastCommand, "").Trim(LineEnding.ToCharArray()).Trim(' ');
        }
    }
}
