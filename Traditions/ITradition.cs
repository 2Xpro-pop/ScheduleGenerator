using System;
using System.Collections.Generic;

using Microsoft.Scripting.Hosting;

using IronPython;
using IronPython.Hosting;
using IronPython.Compiler;
using IronPython.Runtime;

namespace ScheduleGenerator.Traditions
{
    public interface ITradition
    {
        IEnumerable<BaseMarkup> Markup { get; }
        string Name { get; }
        string Description { get; }
        ScriptScope PythonScope { get; }
        
    }
}