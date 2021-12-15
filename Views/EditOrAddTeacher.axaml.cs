using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace ScheduleGenerator.Views
{
    public partial class EditOrAddTeacher : ReactiveUserControl<ViewModels.EditOrAddTeacherVm>
    {
        public EditOrAddTeacher()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
