using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ScheduleGenerator.Models;
using ScheduleGenerator.ViewModels;
using ScheduleGenerator.Views;
using ScheduleGenerator.Traditions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace ScheduleGenerator
{
    public class App : Application
    {
        public static App Instance { get; private set; }

        public static void ErrorMessageBox(string title, string text)
        {
            MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow
            (
                title,
                text,
                icon: MessageBox.Avalonia.Enums.Icon.Error,
                style: MessageBox.Avalonia.Enums.Style.DarkMode
            ).Show();
            
        }

        public override void Initialize()
        {
            Instance = this;
            if(File.Exists("data.json"))
            {
                var data = DataProvider.Load();
                foreach(var group in data.Groups)
                {
                    Groups.Add(group);
                }
                foreach(var teacher in data.Teachers)
                {
                    teacher.Conflicts = new List<int>();
                    Teachers.Add(teacher);
                }
                foreach(var lesson in data.Lessons)
                {
                    Lessons.Add(lesson);
                }
                foreach(var keyValue in data.Schedule)
                {
                    Schedule.Add(keyValue.Key, keyValue.Value);
                }
            }
            AvaloniaXamlLoader.Load(this);
        }

        public void Save()
        {
            DataProvider.Save(Groups.ToList<Group>(), Teachers.ToList<Teacher>(), Lessons.ToList<string>(), Schedule);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        public ObservableCollection<string> Lessons 
        {
            get;
        } = new();
        public ObservableCollection<Models.Group> Groups
        {
            get;
        } = new();

        public ObservableCollection<Teacher> Teachers
        {
            get;
        } =new();

        public AssemblyTraditions AssemblyTraditions { get; } = new AssemblyTraditions();

        // Только для традиций!
        public Settings Settings { get; } = new Settings();

        public Dictionary<string, Week<string>[]>? Schedule 
        { 
            get; 
            set; 
        } = new Dictionary<string, Week<string>[]>();
    }
}