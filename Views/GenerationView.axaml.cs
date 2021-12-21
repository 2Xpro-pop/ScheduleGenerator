using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Avalonia.Controls.Primitives;
using System.ComponentModel;

namespace ScheduleGenerator.Views
{
    public partial class GenerationView : ReactiveUserControl<ViewModels.GenerationVm>
    {
        
        public GenerationView()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();
            ViewModel?.BackgroundWorker.RunWorkerAsync();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}