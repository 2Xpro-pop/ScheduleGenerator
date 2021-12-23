using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Reactive;
using ReactiveUI;

namespace ScheduleGenerator.Windows
{
    public partial class ErrorMessage : ReactiveWindow<ViewModels.ErrorMessageViewModel>
    {
        public ErrorMessage()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            DataContext = this;
            AvaloniaXamlLoader.Load(this);
        }

        public void OkClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}