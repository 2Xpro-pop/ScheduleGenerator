using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Accord.Genetic;

namespace ScheduleGenerator.ViewModels
{
    using Genetic;
    public class GenerationVm: ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "Generation";
        public Generator Generator { get; }
        public BackgroundWorker BackgroundWorker { get; set;}
        public double Progress 
        {
            get => _progress;
            set => this.RaiseAndSetIfChanged(ref _progress, value, nameof(Progress));
        }
        private double _progress;

        public string State 
        { 
            get => _state;
            set => this.RaiseAndSetIfChanged(ref _state, value, nameof(State));
        }
        private string _state;

        public GenerationVm(IScreen screen)
        {
            HostScreen = screen;
            BackgroundWorker = new();
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker.DoWork += StartGeneration;
            Generator = Generator.Build(new ScheduleOptions(App.Instance.Groups, App.Instance.Teachers));
        }

        public void StartGeneration(object? sender, DoWorkEventArgs e)
        {
            while(Generator.FitnessMax < 1)
            {
                if (BackgroundWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                Generator.RunEpoch();
                Progress = Generator.FitnessMax;
            }
            Progress = Generator.FitnessMax;
            App.Instance.Shedule = (Generator.BestChromosome as ShortArrayChromosome)?.Value;
        }

        public void Cancel()
        {
            BackgroundWorker.RunWorkerCompleted += (a,b) =>
            {
                HostScreen.Router.NavigateBack.Execute();
                System.Diagnostics.Trace.WriteLine(Progress);
            };
            BackgroundWorker.CancelAsync();
        }

        public void Apply()
        {
            BackgroundWorker.RunWorkerCompleted += (a,b) =>
            {
                HostScreen.Router.NavigateBack.Execute();
                System.Diagnostics.Trace.WriteLine(Progress);
            };
            BackgroundWorker.CancelAsync();
        }

    }
}