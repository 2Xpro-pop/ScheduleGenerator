using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using System.Diagnostics;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;

namespace ScheduleGenerator.ViewModels
{
    using Traditions;

    public class TraditionsMoreVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsMoreVm";

        public Control Control { get; }

        public string[] Bindings { get; set; }

        public TraditionsMoreVm(IScreen screen, ITradition tradition)
        {
            HostScreen = screen;
            var stack = new StackPanel() {Spacing = 5};
            try
            {
                foreach(var element in tradition.Markup)
                {
                    var rendered = element.Render();
                    if(!string.IsNullOrWhiteSpace(element.Name) &&
                        tradition.PythonScope.ContainsVariable($"observe_{element.Name}"))
                    {
                        dynamic observe = tradition.PythonScope.GetVariable($"observe_{element.Name}");
                        observe(rendered, this);
                    }
                    stack.Children.Add(rendered);
                }
                Control = stack;
            }
            catch
            {
                App.ErrorMessageBox("Ошибка", "Ошибка традиции");
                screen.Router.NavigateBack.Execute();
            }
        }
    }
}
