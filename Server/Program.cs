using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionListener connectionListener = new ConnectionListener();
            connectionListener.SetMediaStorageService(new VMediaStorage());
            connectionListener.Run();
            Console.ReadKey();
        }
    }
}
