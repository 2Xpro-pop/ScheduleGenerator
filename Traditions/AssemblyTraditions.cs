using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using ScheduleGenerator.Models;
using System.Diagnostics;

namespace ScheduleGenerator.Traditions
{
    public class AssemblyTraditions
    {
        public ObservableCollection<ITradition> Traditions { get; } = new ObservableCollection<ITradition>();

        public AssemblyTraditions()
        {
            LoadTriditions();
        }
        
        private void LoadTriditions()
        {
            var directories = Directory.GetDirectories("Traditions");
            foreach(var directory in directories)
            {
                Trace.WriteLine(directory);
                var markupPath = Path.Combine($"{directory}", "markup.stack");
                var pythonPath = Path.Combine($"{directory}", "main.py");
                Trace.WriteLine(File.Exists(markupPath));
                Trace.WriteLine(File.Exists(pythonPath));
                if (File.Exists(markupPath) && 
                    File.Exists(pythonPath))
                {
                    Traditions.Add(
                        new PythonTradition($"{directory}/markup.stack", $"{directory}/main.py")
                    );
                }
                    
            }
        }

    }
}