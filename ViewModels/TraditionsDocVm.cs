using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;

namespace ScheduleGenerator.ViewModels
{
    public class TraditionsDocVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsDocVm";

        public TraditionsDocVm(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
