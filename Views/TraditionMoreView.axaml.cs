using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace ScheduleGenerator.Views
{
    public partial class TraditionMoreView : ReactiveUserControl<ViewModels.TraditionsMoreVm>
    {
        public TraditionMoreView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
