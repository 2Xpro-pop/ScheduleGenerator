using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;

using Microsoft.Scripting.Hosting;

using Newtonsoft.Json;

namespace ScheduleGenerator.Traditions
{
    public class TraditionOnlyInfo : ITradition
    {
        public IEnumerable<BaseMarkup> Markup { get => throw new NotSupportedException(); }
        public string Name { get; set; } = "Not defined";
        public string Description { get; set; } = "Not defined";
        public string Id { get; set; } = string.Empty; 
        public ScriptScope PythonScope { get => throw new NotSupportedException();  } 

        public TraditionOnlyInfo()
        {

        }

        public void Refresh()
        {
            throw new NotSupportedException();
        }

        public static List<ITradition>? GetBestsTraditions()
        {
            var settings = App.Instance.Settings;
            var request = WebRequest.Create($"{settings["api-url"]}/{settings["api-get-traditions"]}");
            var response = request.GetResponse();
            using(var stream = response.GetResponseStream())
            {
                using(var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<List<ITradition>>(reader.ReadToEnd());
                }
            }
        }

    }
}
