using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using ScheduleGenerator.Views;
using ScheduleGenerator.ViewModels;
using ReactiveUI;
using Splat;

namespace ScheduleGenerator
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .Start<MainWindow>( () =>  new MainWindowViewModel());

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() => new Options(), typeof(IViewFor<OptionsViewModel>));
            Locator.CurrentMutable.Register(() => new EditOrAddTeacher(), typeof(IViewFor<EditOrAddTeacherVm>));
            Locator.CurrentMutable.Register(() => new EditOrAddGroup(), typeof(IViewFor<EditOrAddGroupVm>));
            Locator.CurrentMutable.Register(() => new TraditionsView(), typeof(IViewFor<TraditionsVm>));
            Locator.CurrentMutable.Register(() => new TradiotionsDocView(), typeof(IViewFor<TraditionsDocVm>));
            Locator.CurrentMutable.Register(() => new TraditionMoreView(), typeof(IViewFor<TraditionsMoreVm>));

            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }
    }
}
