using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Models
{
    public class Group
    {
        public string Name { get; set; }
        public Dictionary<string,int> NeedLessons { get; set; }
        /// <summary>
        /// BadClock считаеться плохим временем от 0 до 7
        /// </summary>
        public ReadOnlyCollection<int> BadClock { get; set; }
    }
}
