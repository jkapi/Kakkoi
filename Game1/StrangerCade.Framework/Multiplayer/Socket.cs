using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrangerCade.Framework.Multiplayer
{
    public class Client
    {
        public static void StartClient()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            Debug.WriteLine("client connected!!");
            NetworkStream ns = client.GetStream();
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));

            thread.Start(client);

            string s;
            while (Game1.Game1.stopping == false)
            {
                byte[] buffer = Encoding.ASCII.GetBytes("Hallo ");
                ns.Write(buffer, 0, buffer.Length);
                Thread.Sleep(2000);
            }

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            Debug.WriteLine("disconnect from server!!");
        }

        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Debug.WriteLine(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
            }
        }
    }
}
