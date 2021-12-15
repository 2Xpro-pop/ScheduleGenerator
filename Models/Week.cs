using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Models
{
    public class Week<T>
    {
        public T? Monday { get; set; }
        public T? Tuesday { get; set; }
        public T? Wednesday { get; set; }
        public T? Thursday { get; set; }
        public T? Friday { get; set; }
        public T? Saturaday { get; set; }
    }
}
