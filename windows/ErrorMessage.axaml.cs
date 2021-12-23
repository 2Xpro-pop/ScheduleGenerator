using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScheduleGenerator.Windows
{
    public partial class ErrorMessage : Window
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
            AvaloniaXamlLoader.Load(this);
        }
    }
}