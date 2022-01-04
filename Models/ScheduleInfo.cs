using System.Collections.Generic;

namespace ScheduleGenerator.Models
{
    public class ScheduleInfo
    {
        public string Name { get; set; } 
        public Dictionary<string, Week<string>[]> Schedule { get; set; } = new();
    }
}