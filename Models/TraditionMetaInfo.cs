using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGenerator.Models
{
    public class TraditionMetaInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string UniqueId { get; private set; }

        public TraditionMetaInfo(string name, string description, string uniqueId)
        {
            Name = name;
            Description = description;
            UniqueId = uniqueId;
        }
    }
}
