using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;


namespace ScheduleGenerator.Views
{
    public partial class TraditionsView : ReactiveUserControl<ViewModels.TraditionsVm>
    {
        public TraditionsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
