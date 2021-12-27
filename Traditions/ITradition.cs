using System.Collections.Generic;

using Microsoft.Scripting.Hosting;


namespace ScheduleGenerator.Traditions
{
    public interface ITradition
    {
        IEnumerable<BaseMarkup> Markup { get; }
        string Name { get; }
        string Description { get; }
        ScriptScope PythonScope { get; }
        void Refresh();
    }
}