using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ScheduleGenerator.Models;
using ScheduleGenerator.ViewModels;
using ScheduleGenerator.Views;
using ScheduleGenerator.Windows;
using ScheduleGenerator.Traditions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Media;

namespace ScheduleGenerator
{
    public class App : Application
    {
        public static App Instance { get; private set; }
        public static Window MainWindowInstance {get; set;}

        public static async void ErrorMessageBox(string title, string text, Action? onDialogEnd = null)
        {
            var error = new ErrorMessage();
            error.ViewModel = new ErrorMessageViewModel()
            {
                Text = text,
                Title = title,
            };
            SystemSounds.Beep.Play();
            await error.ShowDialog(MainWindowInstance);
            onDialogEnd?.Invoke();
        }

        public override void Initialize()
        {
            Instance = this;
            AvaloniaXamlLoader.Load(this);
        }

        public void Save()
        {
            Settings.Save();
            DataProvider.Save(Groups.ToList<Group>(), Teachers.ToList<Teacher>(), Lessons.ToList<string>(), Schedule);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // desktop.MainWindow = new MainWindow
                // {
                //     DataContext = new MainWindowViewModel(),
                // };
                // MainWindowInstance = desktop.MainWindow;
                desktop.MainWindow = new Windows.Startup();
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

        public AssemblyTraditions AssemblyTraditions { get; set; }

        // Только для традиций!
        public Settings Settings { get; set; } 

        public Dictionary<string, Week<string>[]>? Schedule 
        { 
            get; 
            set; 
        } = new Dictionary<string, Week<string>[]>();
    }
}