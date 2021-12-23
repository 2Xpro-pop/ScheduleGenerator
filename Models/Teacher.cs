using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Models
{
    public class Teacher
    {
        public string Name { get; set; }
        public string Lesson { get; set; }
        /// <summary>
        /// BadTime считаеться плохим временем от 0 до 47
        /// </summary>
        public ReadOnlyCollection<int> BadTimes { get; set; }
        public List<int> Conflicts { get; set; } = new List<int>();
    }
}
