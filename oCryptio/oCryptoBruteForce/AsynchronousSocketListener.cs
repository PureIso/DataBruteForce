#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Linq;

#endregion

namespace oCryptoBruteForce
{
    /// <summary>
    ///     The asynchronous socket listener.
    /// </summary>
    public static class AsynchronousSocketListener
    {
        #region Fields
        private static readonly ManualResetEvent AllDone = new ManualResetEvent(false);
        private static Socket _listener;
        #endregion

        #region Event
        public static event DelegateObjectDelegate OnStartWork;
        public static event DelegateObjectDelegate OnEndWork;
        public static event DelegateObjectDelegate OnClientToServerRequest;
        public static event SetInfoText OnSetTextToInfo;
        #endregion

        #region Public Methods and Operators
        public static void StartListening(int port)
        {
            //This function will block a thread
            if (_listener != null && _listener.IsBound) return;
            IPAddress ipAddress = null;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily != AddressFamily.InterNetwork) continue;
                ipAddress = ip; //Problems if you have virtual machine --NB
            }
            if (ipAddress != null)
            {
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
                _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Bind the socket to the local endpoint and listen for incoming connections.
                _listener.Bind(localEndPoint);
                _listener.Listen(100);
                Listening = true;
                while (Listening)
                {
                    AllDone.Reset();    // Set the event to non signaled state.
                    _listener.BeginAccept(AcceptCallback, _listener);
                    AllDone.WaitOne();  // Wait until a connection is made before continuing.
                }
                _listener = null;
            }
        }
        #endregion

        #region Methods

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            AllDone.Set();
            if (!_listening) return;
            // Get the socket that handles the client request.
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            // Create the state object.
            StateObject state = new StateObject {WorkSocket = handler};
            handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.
            Socket handler = (Socket)ar.AsyncState;
            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            IPEndPoint ipEndPoint = handler.RemoteEndPoint as IPEndPoint;
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.WorkSocket;
            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead <= 0) return;
            state.StringBuilder.Append(Encoding.UTF8.GetString(state.Buffer, 0, bytesRead));
            String content = state.StringBuilder.ToString();
            if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
            {
                content = content.Replace("<EOF>", "");
                byte[] currentData;
                if (content.StartsWith("<OnStartWork>"))
                {
                    #region OnStartWork
                    try
                    {
                        content = content.Replace("<OnStartWork>", "");
                        DelegateObject serializedDelegateObject;
                        byte[] contentBytes = StringToByteArray(content);
                        //De-serializing serialized object
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            memoryStream.Write(contentBytes, 0, contentBytes.Length);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            serializedDelegateObject = (DelegateObject)binaryFormatter.Deserialize(memoryStream);
                        }
                        //Add return Address
                        var ipEndPoint = handler.RemoteEndPoint as IPEndPoint;
                        if (ipEndPoint != null)
                        {
                            ipEndPoint.Port = serializedDelegateObject.ListeningPort;
                            serializedDelegateObject.LocalEndpoint = ipEndPoint;
                        }
                        //Call function with object
                        OnClientToServerRequest(serializedDelegateObject);
                        OnStartWork(serializedDelegateObject);
                        //Reply true
                        currentData = Encoding.UTF8.GetBytes("TRUE");
                        using (MemoryStream ms = new MemoryStream(new byte[currentData.Length + 4]))
                        {
                            byte[] size = BitConverter.GetBytes(currentData.Length);
                            ms.Write(size, 0, 4);
                            ms.Write(currentData, 0, currentData.Length);
                            state.SendBuffer = ms.ToArray();
                            Send(handler, Encoding.UTF8.GetString(state.SendBuffer, 0, state.SendBuffer.Length));
                            ms.Close();
                        }
                    }
                    catch
                    {
                        currentData = Encoding.UTF8.GetBytes("FALSE");
                        using (MemoryStream ms = new MemoryStream(new byte[currentData.Length + 4]))
                        {
                            byte[] size = BitConverter.GetBytes(currentData.Length);
                            ms.Write(size, 0, 4);
                            ms.Write(currentData, 0, currentData.Length);
                            state.SendBuffer = ms.ToArray();
                            Send(handler, Encoding.UTF8.GetString(state.SendBuffer, 0, state.SendBuffer.Length));
                            ms.Close();
                        }
                    }
                    #endregion
                }
                else if (content.StartsWith("<OnEndWork>"))
                {
                    #region OnEndWork
                    try
                    {
                        content = content.Replace("<OnEndWork>", "");
                        DelegateObject serializedDelegateObject;
                        byte[] contentBytes = StringToByteArray(content);
                        //De-serializing serialized object
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            memoryStream.Write(contentBytes, 0, contentBytes.Length);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            serializedDelegateObject = (DelegateObject)binaryFormatter.Deserialize(memoryStream);
                        }
                        //Add return Address
                        //Call function with object
                        Console.WriteLine("Search Finished Checksum = "+serializedDelegateObject.Checksum);
                        //Reply true
                        currentData = Encoding.UTF8.GetBytes("TRUE");
                        using (MemoryStream ms = new MemoryStream(new byte[currentData.Length + 4]))
                        {
                            byte[] size = BitConverter.GetBytes(currentData.Length);
                            ms.Write(size, 0, 4);
                            ms.Write(currentData, 0, currentData.Length);
                            state.SendBuffer = ms.ToArray();
                            Send(handler, Encoding.UTF8.GetString(state.SendBuffer, 0, state.SendBuffer.Length));
                            ms.Close();
                        }
                    }
                    catch
                    {
                        currentData = Encoding.UTF8.GetBytes("FALSE");
                        using (MemoryStream ms = new MemoryStream(new byte[currentData.Length + 4]))
                        {
                            byte[] size = BitConverter.GetBytes(currentData.Length);
                            ms.Write(size, 0, 4);
                            ms.Write(currentData, 0, currentData.Length);
                            state.SendBuffer = ms.ToArray();
                            Send(handler, Encoding.UTF8.GetString(state.SendBuffer, 0, state.SendBuffer.Length));
                            ms.Close();
                        }
                    }
                    #endregion
                }
                else
                {
                    currentData = Encoding.UTF8.GetBytes("Result**Invalid function.");
                    using (MemoryStream ms = new MemoryStream(new byte[currentData.Length + 4]))
                    {
                        byte[] size = BitConverter.GetBytes(currentData.Length);
                        ms.Write(size, 0, 4);
                        ms.Write(currentData, 0, currentData.Length);
                        state.SendBuffer = ms.ToArray();
                        Send(handler, Encoding.UTF8.GetString(state.SendBuffer, 0, state.SendBuffer.Length));
                        ms.Close();
                    }
                }
            }
            else
            {
                // Not all data received. Get more.
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, handler);
        }
        #endregion

        #region Properties

        private static bool _listening;
        public static bool Listening
        {
            get { return _listening; }
            set
            {
                if(!value)_listener.Close();
                _listening = value;
            }
        }

        public static List<string> Connections { get; set; } 
        #endregion
    }

    public delegate void SetInfoText(string text);
}