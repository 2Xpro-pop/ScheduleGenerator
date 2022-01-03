using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ScheduleGenerator.Controls
{
    public partial class EditableList : UserControl
    {
        public EditableList()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}