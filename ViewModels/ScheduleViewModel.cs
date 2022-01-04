using System.Linq;
using System.Collections.Generic;

using ReactiveUI;

namespace ScheduleGenerator.ViewModels
{
    public class ScheduleViewModel: ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsDocVm";
        public Models.ScheduleInfo ScheduleInfo { get;}
        public Models.Week<string>[] Schedule 
        { 
            get => _schedule;
            set => this.RaiseAndSetIfChanged(ref _schedule, value, nameof(Schedule));
        }
        private Models.Week<string>[] _schedule;
        public List<string> Groups { get; }

        public int SelectedIndex
        {
            get => _index;
            set 
            {
                Schedule = ScheduleInfo.Schedule[Groups[value]] ?? GetNull();
                this.RaiseAndSetIfChanged(ref _index, value, nameof(SelectedIndex));
            }
        }
        private int _index = 0;

        public ScheduleViewModel(IScreen screen, Models.ScheduleInfo scheduleInfo)
        {
            HostScreen = screen;
            ScheduleInfo = scheduleInfo;
            Groups = new List<string>(ScheduleInfo.Schedule.Keys);
            Schedule = scheduleInfo.Schedule[Groups[SelectedIndex]];
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