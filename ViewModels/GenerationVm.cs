using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleGenerator.ViewModels
{
    public class GenerationVm: ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "Generation";

        public GenerationVm(IScreen screen)
        {
            HostScreen = screen;
        }
        
    }
}