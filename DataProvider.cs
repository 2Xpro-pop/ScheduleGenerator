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
        public Dictionary<string, Week<string>[]> Schedule{get; set;}

        public static DataProvider Load()
        {
            var data = JsonConvert.DeserializeObject<DataProvider>(File.ReadAllText("data.json"));
            data = data ?? new DataProvider();
            data.Groups = data.Groups ?? new Group[0];
            data.Teachers = data.Teachers ?? new Teacher[0];
            data.Lessons = data.Lessons ?? new string[0];
            data.Schedule = data.Schedule ?? new();
            return data;
        }

        public static void Save(ICollection<Group> groups, ICollection<Teacher> teachers, ICollection<string> lessons, Dictionary<string, Week<string>[]> schedule)
        {
            using(StreamWriter file = File.CreateText("data.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, new DataProvider()
                {
                    Groups = groups,
                    Teachers = teachers,
                    Lessons = lessons,
                    Schedule = schedule
                });
            }
        }
    }
}