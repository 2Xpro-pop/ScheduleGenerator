using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScheduleGenerator.Views
{
    public partial class ScheduleView : UserControl
    {
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}