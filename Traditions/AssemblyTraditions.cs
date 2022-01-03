using System.Linq;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ScheduleGenerator.Models;
using System.Diagnostics;

namespace ScheduleGenerator.Traditions
{
    public class AssemblyTraditions
    {
        public ObservableCollection<ITradition> Traditions { get; } = new ObservableCollection<ITradition>();
        public List<ITradition> ErrorTraditions { get; set; } = new List<ITradition>();
        public AssemblyTraditions()
        {
            LoadTriditions();
        }
        public void Refresh()
        {
            LoadTriditions();
        }       
        private void LoadTriditions()
        {
            Traditions.Clear();
            ErrorTraditions.Clear();
            var directories = Directory.GetDirectories("Traditions");
            foreach(var directory in directories)
            {
                Trace.WriteLine(directory);
                var markupPath = Path.Combine($"{directory}", "markup.stack");
                var pythonPath = Path.Combine($"{directory}", "main.py");
                if (File.Exists(markupPath) && 
                    File.Exists(pythonPath))
                {
                    Traditions.Add(
                        new PythonTradition($"{directory}/markup.stack", $"{directory}/main.py", Traditions.Count)
                    );
                }
                    
            }
            Trace.WriteLine("End");
        }

        //Возвращает значение от 0 до 1
        public double CountPoints(ushort[] schedule)
        {
            var points = 0d;
            foreach(var tradition in Traditions)
            {
                var scope = tradition.PythonScope;
                if(ErrorTraditions.Contains(tradition))
                {
                    continue;
                }
                try
                {
                    if(scope.ContainsVariable("count_point"))
                    {
                        points += scope.GetVariable("count_point")(schedule);
                    }
                    else
                    {
                        points += 1;
                    }
                }
                catch(Exception exc)
                {
                    ErrorTraditions.Add(tradition);
                    throw new TraditonException(tradition, exc);
                }
            }
            return points / (Traditions.Count-ErrorTraditions.Count);
        }
    }
}