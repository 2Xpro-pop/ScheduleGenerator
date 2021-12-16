using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;

namespace ScheduleGenerator.ViewModels
{
    public class TraditionsVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsVm";

        public TraditionsVm(IScreen screen)
        {
            HostScreen = screen;
            HostScreen.Router.NavigationStack.Clear();
        }

        public void Edit(string unique)
        {
            HostScreen.Router.Navigate.Execute( new TraditionsMoreVm(HostScreen, App.Instance.AssemblyTraditions.GetTraditionMarkUp(unique)));
        }

        public IEnumerable<TraditionMetaInfo> MetaInfos 
        {
            get;
        } = App.Instance.AssemblyTraditions.GetTraditionsMetaInfos();
    }
}
