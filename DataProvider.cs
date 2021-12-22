using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleGenerator
{
    using Models;
    public class DataProvider
    {
        public ICollection<Group> Groups { get; set;}
        public ICollection<Teacher> Teachers {get; set;}
        public ICollection<string> Lessons {get; set;}

        public static DataProvider Load()
        {
            return JsonConvert.DeserializeObject<DataProvider>(File.ReadAllText("data.json"));
        }

        public static void Save(ICollection<Group> groups, ICollection<Teacher> teachers, ICollection<string> lessons)
        {
            using(StreamWriter file = File.CreateText("data.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, new DataProvider()
                {
                    Groups = groups,
                    Teachers = teachers,
                    Lessons = lessons,
                });
            }
        }
    }
}