using System.Collections.Generic;

using Microsoft.Scripting.Hosting;

using Newtonsoft.Json;

namespace ScheduleGenerator.Traditions
{
    public interface ITradition
    {
        [JsonIgnore]
        IEnumerable<BaseMarkup> Markup { get; }
        string Name { get; }
        string Description { get; }
        [JsonIgnore]
        ScriptScope PythonScope { get; }
        void Refresh();
    }
}