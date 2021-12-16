using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;


namespace ScheduleGenerator.Views
{
    public partial class TradiotionsDocView : ReactiveUserControl<ViewModels.TraditionsDocVm>
    {
        public TradiotionsDocView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
