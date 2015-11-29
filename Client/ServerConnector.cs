using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ServerConnector
    {
        Int32 port;
        IPAddress localAddr;
        TcpClient client;
        NetworkStream stream;

        public ServerConnector(string serverAddress = "192.168.0.102")
        {
            port = 3737;
            localAddr = IPAddress.Parse(serverAddress);
            client = new TcpClient(serverAddress, port);
            stream = client.GetStream();
        }

        public void Send(string toSend)
        {
            Byte[] data;

            data = System.Text.Encoding.ASCII.GetBytes(toSend);
            stream.Write(data, 0, toSend.Length);
            //Console.Write("Sent: ");
            //for (int i = 0; i < data.Length; i++)
            //{
            //    Console.Write(Convert.ToChar(data[i]));
            //}
            //Console.WriteLine();
            //response = new Byte[256];
            //String responseData = String.Empty;
            //List<string> toWrite = new List<string>();

            //Console.WriteLine("\nPress Enter to continue...");
            
            //Console.Read();

        }

        public void CloseConnection()
        {
            stream.Close();
            client.Close();
        }
    }
}
