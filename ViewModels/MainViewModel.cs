using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using System.Data;
using ReactiveUI;

namespace ScheduleGenerator.ViewModels
{
    public class MainViewModel : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "main";
        public static readonly Random random = new Random();
        public static readonly string[] loremIpsum = new string[] { 
            "Математика", 
            "Английский", 
            "Немецкий",
            "Языки прогаммирование",
            "Вчк",
            "Аисд",
        };

        public Models.Week[] Schedule { get; set; }

        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;
            Schedule = new Models.Week[8];

            for(int i=0; i < 8; i++ )
            {
                Schedule[i] = new Models.Week();
                Schedule[i].Monday = LoremIpsumLesson();
                Schedule[i].Tuesday = LoremIpsumLesson();
                Schedule[i].Wednesday = LoremIpsumLesson();
                Schedule[i].Thursday = LoremIpsumLesson();
                Schedule[i].Friday = LoremIpsumLesson();
                Schedule[i].Saturaday = LoremIpsumLesson();
            }

        }

        public string LoremIpsumLesson()
        {
            return loremIpsum[random.Next(6)];
        }

    }
}
