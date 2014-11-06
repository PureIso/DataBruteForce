#region

using System.Net.Sockets;
using System.Text;

#endregion

namespace oCryptoBruteForce
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        #region Constants
        public const int BufferSize = 4096;
        #endregion

        #region Fields

        public readonly byte[] Buffer = new byte[BufferSize]; // Receiver buffer

        public readonly StringBuilder StringBuilder = new StringBuilder(); // Received data string.

        public byte[] SendBuffer = new byte[0]; // Sending Buffer

        public Socket WorkSocket = null; // Socket Client

        #endregion
    }
}