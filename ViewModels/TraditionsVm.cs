using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;
using ScheduleGenerator.Traditions;

namespace ScheduleGenerator.ViewModels
{
    public class TraditionsVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsVm";

        public TraditionsVm(IScreen screen)
        {
            HostScreen = screen;
        }

        public void Edit(ITradition tradition)
        {
            HostScreen.Router.Navigate.Execute(new TraditionsMoreVm(HostScreen, tradition));
        }

        public ObservableCollection<ITradition> MetaInfos 
        {
            get;
        } = App.Instance.AssemblyTraditions.Traditions;
    }
}
