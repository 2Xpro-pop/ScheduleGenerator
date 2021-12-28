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
        public ScriptScope PythonScope { get; private set; } = python.CreateScope();
        private readonly string _markupPath;
        private readonly string _pythonPath;

        public PythonTradition(string markupPath, string pythonPath, int id)
        {
            _markupPath = markupPath;
            _pythonPath = pythonPath;

            PythonScope.SetVariable("__id__", id);

            LoadFiles();
        }

        public void Refresh() => LoadFiles();
        private void LoadFiles()
        {
            var pathes = python.GetSearchPaths();
            pathes.Add(Directory.GetDirectoryRoot(_pythonPath));
            python.SetSearchPaths(pathes);
            PythonScope = python.ExecuteFile(_pythonPath, PythonScope);

            var doc = BaseMarkup.Document;
            doc.Elements.Clear();
            doc.Load(_markupPath);

            Markup = doc.Elements.Cast<BaseMarkup>();

            pathes.Remove(Directory.GetDirectoryRoot(_pythonPath));
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
