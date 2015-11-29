using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace Server
{
    class VMediaStorage : MediaStorage
    {
        int appId = 5166444;
        Settings settings = Settings.All;
        VkApi api;
        System.Collections.ObjectModel.ReadOnlyCollection<VkNet.Model.Attachments.Audio> musicList;

        public VMediaStorage()
        {
            api = new VkApi();
        }

        public void Authenticate(String login, String password)
        {
            api.Authorize(appId, login, password, settings);
        }

        public List<OutputFile> GetFileList(Int32 maxCount)
        {
            musicList = api.Audio.Get((long)api.UserId, count: maxCount);

            foreach (var audio in musicList)
            {
                Console.WriteLine(audio.Id + "\t" + audio.Artist + "\t" + audio.Title + "\t" + audio.Url);
            }
            List<OutputFile> output = musicList
                                        .Select(x => new OutputFile() { Id = x.Id, fileName = x.Artist + "\t" + x.Title })
                                        .ToList(); ;
            return output;
        }

        public void GetFile(Int32 Id)
        {
            Console.WriteLine("Downloading...");
            using (var client = new WebClient())
            {
                client.DownloadFile(musicList[Id].Url, musicList[Id].Artist + " - " + musicList[Id].Title + ".mp3");
            }
            Console.WriteLine("Seems to be downloaded.");
        }
    }
}
