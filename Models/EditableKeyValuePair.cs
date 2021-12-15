using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Models
{
    public class EditableKeyValuePair<Tkey, TValue>
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
    }
}
