﻿#region

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

#endregion

namespace oCryptoBruteForce
{
    /// <summary>
    ///     The asynchronous socket listener.
    /// </summary>
    public static class AsynchronousSocketListener
    {
        private static readonly ManualResetEvent AllDone = new ManualResetEvent(false);
        #region Public Methods and Operators
        public static void StartListening(int port)
        {
            //This function will block a thread
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
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Bind the socket to the local endpoint and listen for incoming connections.
                listener.Bind(localEndPoint);
                listener.Listen(100);
                while (true)
                {
                    AllDone.Reset();    // Set the event to non signaled state.
                    listener.BeginAccept(AcceptCallback, listener);
                    AllDone.WaitOne();  // Wait until a connection is made before continuing.
                }
            }
        }
        #endregion

        #region Methods

        private static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            AllDone.Set();
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
            if (bytesRead > 0)
            {
                state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));
                String content = state.StringBuilder.ToString();
                if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
                {
                    content = content.Replace("<EOF>", "");
                    byte[] currentData;
                    if (content.StartsWith("Images"))
                    {
                    }
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, handler);
        }

        #endregion
    }
}