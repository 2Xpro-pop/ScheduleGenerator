using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;;

namespace ScheduleGenerator.ViewModels
{
    public class OptionsViewModel : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "Options";

        public OptionsViewModel(IScreen screen) => HostScreen = screen;
        
    }
}
