using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Avalonia.Controls.Primitives;
using System.ComponentModel;
using System;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            ViewModel.BackgroundWorker.RunWorkerAsync();
        }
        
    }
}