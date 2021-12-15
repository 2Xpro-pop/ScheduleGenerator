using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ScheduleGenerator.Models;
using ScheduleGenerator.ViewModels;
using ScheduleGenerator.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public void Save()
        {
            
        }

        public void Generate()
        {

        }

        public override void Initialize()
        {
            Instance = this;
            AvaloniaXamlLoader.Load(this);
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
        } = new(new string[] { 
                "Математика", 
                "Английский", 
                "Немецкий",
                "Языки прогаммирование",
                "Вчк",
                "Аисд",
            });
        public ObservableCollection<Models.Group> Groups
        {
            get;
        } = new(new Group[] 
            {
                new Group() { Name = "Min-1-21"},
                new Group() { Name = "Ain-1-21"},
                new Group() { Name = "Ain-2-21"},
                new Group() { Name = "Ain-3-21"},
                new Group() { Name = "Win-1-21"},
            });

        public ObservableCollection<Teacher> Teachers
        {
            get;
        } =new(new Teacher[] 
        {
            new Teacher() { Name = "Arnold"},
            new Teacher() { Name = "Bob"},
            new Teacher() { Name = "Lorem"},
            new Teacher() { Name = "Ipsum"},
        });

    }
}