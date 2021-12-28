using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Avalonia.Controls.Primitives;
using System.ComponentModel;
using System;

namespace ScheduleGenerator.Windows
{
    public partial class GenerationView : ReactiveWindow<ViewModels.GenerationVm>
    {
        
        public GenerationView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ViewModel.BackgroundWorker.Dispose();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            ViewModel.BackgroundWorker.RunWorkerAsync();
            ViewModel.BackgroundWorker.RunWorkerCompleted += (a,b) =>
            {
                Close(true);
            };
        }
        
    }
}