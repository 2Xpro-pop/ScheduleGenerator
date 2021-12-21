using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Scripting.Hosting;

using IronPython;
using IronPython.Hosting;
using IronPython.Compiler;
using IronPython.Runtime;

namespace ScheduleGenerator.Traditions
{
    public class PythonTradition : ITradition
    {
        public readonly static ScriptEngine python;

        static PythonTradition()
        {
            python = Python.CreateEngine();
            python.Runtime.Globals.SetVariable("App", App.Instance);
            
        }

        public IEnumerable<BaseMarkup> Markup { get; private set; }
        public string Name { get; private set; } = "Not defined";
        public string Description { get; private set; } = "Not defined";
        public ScriptScope PythonScope { get; private set; }

        public PythonTradition(string markupPath, string pythonPath)
        {
            PythonScope = python.CreateScope();
            var pathes = python.GetSearchPaths();
            pathes.Add(Directory.GetDirectoryRoot(pythonPath));
            python.SetSearchPaths(pathes);
            PythonScope = python.ExecuteFile(pythonPath, PythonScope);

            var doc = BaseMarkup.Document;
            doc.Load(markupPath);

            Markup = doc.Elements.Cast<BaseMarkup>();

            pathes.Remove(Directory.GetDirectoryRoot(pythonPath));
            python.SetSearchPaths(pathes);

            if(PythonScope.ContainsVariable("__name__"))
            {
                Name = PythonScope.GetVariable<string>("__name__");
            }
            if(PythonScope.ContainsVariable("__description__"))
            {
                Description = PythonScope.GetVariable<string>("__description__");
            }
        }
    }
}
