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
        public Models.Week<string>[] Schedule 
        { 
            get => _weeks;
            set
            {
                this.RaiseAndSetIfChanged(ref _weeks, value, nameof(Schedule));
            }
        }
        private Models.Week<string>[] _weeks;
        public List<string> Groups { get; } = new(App.Instance.Groups.Select( f => f.Name));

        public int SelectedIndex
        {
            get => _index;
            set 
            {
                Schedule = App.Instance.Schedule[Groups[value]] ?? GetNull();
                this.RaiseAndSetIfChanged(ref _index, value, nameof(SelectedIndex));
            }
        }
        private int _index = 0;

        public MainViewModel(IScreen screen)
        {
            HostScreen = screen;
            try
            {
                Schedule = App.Instance.Schedule[Groups[0]];
            }
            catch
            {
                
            }
            finally
            {
                Schedule = Schedule ?? GetNull();
            }
        }

        public Models.Week<string>[] GetNull()
        {
            var week = new Models.Week<string>[8];
            for(int i=0; i < 8; i++)
            {
                week[i] = new Models.Week<string>();
            }
            return week;
        }

    }
}
