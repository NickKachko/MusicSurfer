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
        private Int32 lastClientId;
        private IPAddress serverAddress;
        private Byte[] bytes;
        private String data;
        private Int32 maxConnections;
        private Thread listeningThread;
        private List<Client> listOfClients;
        private MediaStorage mediaStorageEntity;


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

        private void clientProcess()
        {
            Client currentClient = listOfClients[listOfClients.Count - 1];
            NetworkStream stream = currentClient.tcpClient.GetStream();
            try
            {
                while (true)
                {
                    int i = stream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    if (data == "")
                        break;
                    Console.WriteLine("From #" + currentClient.Id + ": " + data);
                }
            }
            finally
            {
                Console.WriteLine("Client #" + currentClient.Id + " disconnected. " + currentClient.remoteIpEndPoint.Address);
                stream.Close();
                currentClient.tcpClient.Close();
            }
        }

        private void AcceptConnection()
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection... ");
                TcpClient client = server.AcceptTcpClient();
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("Connected a client with IP " + remoteIpEndPoint.Address + " on port number " + remoteIpEndPoint.Port);
                Client clientToAdd = new Client()
                {
                    Id = lastClientId++,
                    clientThread = new Thread(new ThreadStart(clientProcess)),
                    remoteIpEndPoint = remoteIpEndPoint,
                    tcpClient = client
                };
                listOfClients.Add(clientToAdd);
                clientToAdd.clientThread.Start();
            }
        }

        public ConnectionListener(int _maxConnections = 10)
        {
            serverAddress = GetServerIp();
            InitNet();
            listeningThread = new Thread(new ThreadStart(AcceptConnection));
            listOfClients = new List<Client>();
            maxConnections = _maxConnections;
            lastClientId = 0;
        }

        public void SetMediaStorageService(MediaStorage _mediaStorageEntity)
        {
            mediaStorageEntity = _mediaStorageEntity;
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

    struct Client
    {
        public Thread clientThread;
        public TcpClient tcpClient;
        public IPEndPoint remoteIpEndPoint;
        public Int32 Id;
    }
}
