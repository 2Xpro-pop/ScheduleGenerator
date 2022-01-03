using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive;
using System.Linq;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;
using ScheduleGenerator.Traditions;
using Avalonia.Threading;

namespace ScheduleGenerator.ViewModels
{
    using Traditions;
    public class TraditionsVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsVm";

        public bool CanDownload
        {
            get => _canDownload;
            set => this.RaiseAndSetIfChanged(ref _canDownload, value, nameof(CanDownload));
        } 
        bool _canDownload = true;

        public TraditionsVm(IScreen screen)
        {
            HostScreen = screen;
            Task.Run
            (
                () => Recomendation = TraditionOnlyInfo.GetBestsTraditions(MetaInfos.Select(f => f.Name).ToList())
            );
        }

        public void Edit(ITradition tradition)
        {
            HostScreen.Router.Navigate.Execute(new TraditionsMoreVm(HostScreen, tradition));
        }
        
        public void Install(ITradition tradition)
        {
            CanDownload = false;
            Task.Run(
                () =>
                {
                    try
                    {
                        TraditionIstaller.Install(tradition.Name);
                    }
                    catch
                    {
                        Dispatcher.UIThread.InvokeAsync(
                            () => 
                            {
                                App.ErrorMessageBox("", $"Не удалось загрузить традицию {tradition.Name}");
                                Refresh();
                            }
                        );
                    }
                    CanDownload = true;
                }
            );
        }

        public void Refresh()
        {
            Recomendation = null;
            Task.Run(
                () => 
                {
                    App.Instance.AssemblyTraditions.Refresh();
                    Recomendation = TraditionOnlyInfo.GetBestsTraditions(MetaInfos.Select(f => f.Name).ToList());
                }
            );
        }

        public ObservableCollection<ITradition> MetaInfos 
        {
            get;
        } = App.Instance.AssemblyTraditions.Traditions;

        public IEnumerable<ITradition> Recomendation 
        {
            get => _recomendation;
            private set => this.RaiseAndSetIfChanged(ref _recomendation, value, nameof(Recomendation));
        } 
        private IEnumerable<ITradition> _recomendation;
    }
}
