using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace ScheduleGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public string Greeting => "Welcome to Avalonia!";

        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public ReactiveCommand<Unit, IRoutableViewModel> GoToMain { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToOption { get; }

        public MainWindowViewModel()
        {
            GoToMain = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new MainViewModel(this))
            );
            GoToOption = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new OptionsViewModel(this))
            );
        }

    }
}
