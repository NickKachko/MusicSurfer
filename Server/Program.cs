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
            int appId = 5166444;
            int audioId = 2;
            string email = "justmynk@gmail.com";
            string password = "";
            Settings settings = Settings.All;

            //var api = new VkApi();
            //api.Authorize(appId, email, password, settings);

            //var music = api.Audio.Get((long)api.UserId, count: 10);
            
            
            //foreach (var audio in music)
            //{
            //    Console.WriteLine(audio.Id + "\t" + audio.Artist + "\t" + audio.Title);
            //}
            //Console.WriteLine("Downloading...");
            //using (var client = new WebClient())
            //{
            //    client.DownloadFile(music[audioId].Url, music[audioId].Artist + " - " + music[audioId].Title + ".mp3");
            //}
            //Console.WriteLine("Seems to be downloaded.");

            ConnectionListener connectionListener = new ConnectionListener();
            connectionListener.Run();
            Console.ReadKey();
        }
    }
}
