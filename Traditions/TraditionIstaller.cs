using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace ScheduleGenerator.Traditions
{
    public static class TraditionIstaller
    {
        public static void Install(string name)
        {
            var settings = App.Instance.Settings;
            var url = $"{settings["api-url"]}{settings["api-get-tradition"]}/{name}";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            using(var stream = response.GetResponseStream() )
            {
                using(var file = File.Create($"Traditions/{name}.tradition"))
                {
                    stream.CopyTo(file);
                }
            }
            ZipFile.ExtractToDirectory($"Traditions/{name}.tradition", $"Traditions/{name}");
        }
    }
}