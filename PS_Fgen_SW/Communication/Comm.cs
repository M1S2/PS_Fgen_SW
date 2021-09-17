using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Fgen_SW.Communication
{
    /// <summary>
    /// Abstract base class for all communication classes
    /// </summary>
    public abstract class Comm
    {
        /// <summary>
        /// Last command string that was written over the communication interface
        /// </summary>
        protected string _lastCommand;

        /// <summary>
        /// Line Ending used when writing
        /// </summary>
        public static string LineEnding = "\r\n";

        /// <summary>
        /// Open the communication interface
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Close the communication interface
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Write to the communication interface
        /// </summary>
        /// <param name="message">message string to write (without the LineEnding, is appended automatically)</param>
        public abstract void Write(string message);

        /// <summary>
        /// Read one line from the communication interface (until the LineEnding is reached)
        /// </summary>
        /// <returns>Read string</returns>
        public abstract string ReadLine();
    }
}
