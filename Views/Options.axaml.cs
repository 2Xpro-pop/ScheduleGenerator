using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace ScheduleGenerator.Views
{
    public partial class Options : ReactiveUserControl<ViewModels.OptionsViewModel>
    {
        public Options()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void TeacherSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (ViewModels.OptionsViewModel)DataContext;
            if((sender as ListBox).SelectedItem != null)
            {
                ViewModel.CanRemoveTeacher = true;
                ViewModel.CanEditTeacher = true;
            }
        }

        private void GroupSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListBox).SelectedItem != null)
            {
                ViewModel.CanEditGroup = true;
                ViewModel.CanRemoveGroup = true;
            }
        }
    }
}
