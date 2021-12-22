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

        public int SelectedIndex
        {
            get => _index;
            set => this.RaiseAndSetIfChanged(ref _index, value, nameof(SelectedIndex));
        }
        private int _index = 0;

        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;
            Schedule = new Models.Week<string>[8];
            var app = App.Instance;
            if(app.Shedule == null)
            {
                return;
            }
            for(int i=0+(48*_index); i< 48*(1+_index); i++)
            {
                var lesson = app.Shedule[i];
                if(app.Teachers.Count < lesson)
                {
                    var teacher = app.Teachers[lesson];
                    var week = i % 48;
                    var day = week % 8;
                    if(week < 8)
                    {
                        Schedule[day].Monday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(week < 16)
                    {
                        Schedule[day].Tuesday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(week < 24)
                    {
                        Schedule[day].Wednesday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(week < 32)
                    {
                        Schedule[day].Thursday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(week < 40)
                    {
                        Schedule[day].Friday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(week < 48)
                    {
                        Schedule[day].Saturaday = $"{teacher.Lesson}({teacher.Name})";
                    }
                }
            }
        }

        public string LoremIpsumLesson()
        {
            var loremIpsum = new List<string>(App.Instance.Lessons);
            return loremIpsum[random.Next(loremIpsum.Count)];
        }

    }
}
