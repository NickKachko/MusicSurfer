using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class ConnectionListener
    {
        private TcpListener server;
        private Int32 serverPort;
        private IPAddress serverAddress;
        private Byte[] bytes, response;
        private String data;
        private Int32 maxConnections;
        private Thread listeningThread;


        private void InitNet()
        {
            serverPort = 3737;
            server = new TcpListener(serverAddress, serverPort);
            server.Start();
            bytes = new Byte[256];
            data = null;
        }

        private IPAddress GetServerIp()
        {
            IPHostEntry host;
            IPAddress localIP = null;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip;
                }
            }
            return localIP;
        }

        private void AcceptConnection()
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection... ");
                TcpClient client = server.AcceptTcpClient();
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("Connected a client with IP " + remoteIpEndPoint.Address + " on port number " + remoteIpEndPoint.Port);
                NetworkStream stream = client.GetStream();

                Console.WriteLine();

                int i = stream.Read(bytes, 0, bytes.Length);
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                client.Close();
            }
        }

        public ConnectionListener(int _maxConnections = 10)
        {
            serverAddress = GetServerIp();
            InitNet();
            listeningThread = new Thread(new ThreadStart(AcceptConnection));
            maxConnections = _maxConnections;
        }

        public void Run()
        {
            listeningThread.Start();
            Console.WriteLine("Server started with IP " + serverAddress.ToString());
        }

        public void Stop()
        {
            listeningThread.Abort();
        }
    }
}
