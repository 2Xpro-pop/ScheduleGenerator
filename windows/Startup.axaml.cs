using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;

using System.IO;
using System.ComponentModel;
using System.Collections.Generic;

namespace ScheduleGenerator.Windows
{
    public partial class Startup : Window
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public Startup()
        {
            InitializeComponent();
            worker.DoWork += (a,b) => InitializeModels();
            worker.RunWorkerCompleted += (a,b) =>
            {
                Close();
            };
            worker.RunWorkerAsync();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Run(
                new Views.MainWindow()
                {
                    ViewModel = new(),
                }
            );
        }

        private void InitializeModels()
        {
            var app = App.Instance;
            if(File.Exists("data.json"))
            {
                
                var data = DataProvider.Load();
                foreach(var group in data.Groups)
                {
                    app.Groups.Add(group);
                }
                foreach(var teacher in data.Teachers)
                {
                    teacher.Conflicts = new List<int>();
                    app.Teachers.Add(teacher);
                }
                foreach(var lesson in data.Lessons)
                {
                    app.Lessons.Add(lesson);
                }
                foreach(var keyValue in data.Schedule)
                {
                    app.Schedule.Add(keyValue.Key, keyValue.Value);
                }
            }

            app.AssemblyTraditions = new Traditions.AssemblyTraditions();

            app.Settings = new Settings();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
