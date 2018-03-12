using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrangerCade.Framework.Multiplayer
{
    /// <summary>
    /// The client networking handler
    /// </summary>
    public class MultiplayerClient
    {
        public static bool Connected = false;
        private static TcpClient client;
        private static Thread RecieveThread;
        public static string RecieveString = "";
        public static void StartClient()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            client = new TcpClient();
            client.Connect(ip, port);
            Debug.WriteLine("client connected!!");
            Connected = true;
            NetworkStream ns = client.GetStream();
            RecieveThread = new Thread(o => ReceiveData((TcpClient)o));

            RecieveThread.Start(client);
        }

        public static void SendData(string message)
        {
            if (Connected)
            {
                NetworkStream ns = client.GetStream();
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                ns.Write(buffer, 0, buffer.Length);
            }
        }

        public static void Disconnect()
        {
            if (Connected)
            {
                client.Client.Shutdown(SocketShutdown.Send);
                Connected = false;
                RecieveThread.Join();
                client.GetStream().Close();
                client.Close();
                Debug.WriteLine("disconnect from server!!");
            }
        }

        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                RecieveString += Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
            }
        }
    }
}
