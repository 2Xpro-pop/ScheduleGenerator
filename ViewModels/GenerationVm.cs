using System;
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

        private bool _apply = false;

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
            if(!BackgroundWorker.CancellationPending || _apply)
            {
                App.Instance.Schedule.Clear();
                var span = new Span<ushort>((Generator.BestChromosome as ShortArrayChromosome)?.Value);
                for(int i = 0; i < span.Length / 48; i++)
                {
                    var name = App.Instance.Groups[i].Name;
                    var shedule = App.Instance.Schedule;
                    shedule.Add(
                        App.Instance.Groups[i].Name, 
                        new Week<string>[8]
                    );
                    for(int week=0; week < 8; week++)
                    {
                        shedule[name][week] = new Week<string>(); 
                    }
                    App.Instance.Schedule = shedule;
                    
                    BuildGeneration(span.Slice(i*48, 48), shedule[name]);
                }
                BackgroundWorker.RunWorkerCompleted += (a,b) =>
                {
                    HostScreen.Router.NavigateBack.Execute();
                };
            }
            
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
            _apply = true;
            BackgroundWorker.RunWorkerCompleted += (a,b) =>
            {
                HostScreen.Router.NavigateBack.Execute();
                System.Diagnostics.Trace.WriteLine(Progress);
            };
            BackgroundWorker.CancelAsync();
        }

        private void BuildGeneration(Span<ushort> shedule, Week<string>[] stringShedule)
        {
            var app = App.Instance;
            for(int i=0; i< 48; i++)
            {
                var lesson = shedule[i];
                if(app.Teachers.Count > lesson)
                {
                    var teacher = app.Teachers[lesson];
                    var day = lesson % 8;

                    if(i < 8)
                    {
                        stringShedule[day].Monday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(i < 16)
                    {
                        stringShedule[day].Tuesday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(i < 24)
                    {
                        stringShedule[day].Wednesday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(i < 32)
                    {
                        stringShedule[day].Thursday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(i < 40)
                    {
                        stringShedule[day].Friday = $"{teacher.Lesson}({teacher.Name})";
                    }
                    else if(i < 48)
                    {
                        stringShedule[day].Saturaday = $"{teacher.Lesson}({teacher.Name})";
                    }
                }
            }
        }

    }
}