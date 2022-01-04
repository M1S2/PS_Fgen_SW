using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Fgen_SW.Interfaces
{
    /// <summary>
    /// Interface for all communication classes
    /// </summary>
    public interface IComm
    {
        /// <summary>
        /// Is the communication channel connected or not
        /// </summary>
        bool Connected { get; set; }

        /// <summary>
        /// Connect to the communication interface
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect from the communication interface
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Write to the communication interface
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, is appended automatically)</param>
        void Write(string message);

        /// <summary>
        /// Read one line from the communication interface (until the LineEnding is reached)
        /// </summary>
        /// <returns>Read string</returns>
        string ReadLine();
    }
}
