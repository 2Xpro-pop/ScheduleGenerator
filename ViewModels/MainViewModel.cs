using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using System.Data;
using ReactiveUI;
using System.Linq;

namespace ScheduleGenerator.ViewModels
{
    public class MainViewModel : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "main";
        public static readonly Random random = new Random();
        public Models.Week<string>[] Schedule { get; set; }

        public IEnumerable<string> Groups { get; } = App.Instance.Groups.Select( f => f.Name);

        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;
            Schedule = new Models.Week<string>[8];

            for(int i=0; i < 8; i++ )
            {
                Schedule[i] = new Models.Week<string>();
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
            var loremIpsum = new List<string>(App.Instance.Lessons);
            return loremIpsum[random.Next(loremIpsum.Count)];
        }

    }
}
