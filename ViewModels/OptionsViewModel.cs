using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;

namespace ScheduleGenerator.ViewModels
{
    public class OptionsViewModel : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "Options";
        public OptionsViewModel(IScreen screen)
        {
            HostScreen = screen;
            RemoveLessonCommand = ReactiveCommand.Create(RemoveLesson);
        }

        public ReactiveCommand<Unit, Unit> RemoveLessonCommand { get; }

        public ObservableCollection<Teacher> Teachers
        {
            get;
        } =  App.Instance.Teachers;

        public bool CanRemoveTeacher 
        {
            get => _canRemoveTeacher;
            set => this.RaiseAndSetIfChanged(ref _canRemoveTeacher, value, nameof(CanRemoveTeacher));
        }
        private bool _canRemoveTeacher = false;

        public bool CanEditTeacher 
        {
            get => _canEditTeacher;
            set => this.RaiseAndSetIfChanged(ref _canEditTeacher, value, nameof(CanEditTeacher));
        }
        private bool _canEditTeacher = false;

        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value, nameof(SelectedTeacher));
        }
        private Teacher _selectedTeacher;

        public void RemoveTeacher()
        {
            Teachers.Remove(SelectedTeacher);
            CanEditTeacher = false;
            CanRemoveTeacher = false;
        }

        public void AddTeacher()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddTeacherVm(HostScreen));
        }

        public void EditTeacher()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddTeacherVm(HostScreen, SelectedTeacher));
        }

        public ObservableCollection<Group> Groups
        {
            get;
        } = App.Instance.Groups;

        public bool CanRemoveGroup 
        {
            get => _canRemoveGroup;
            set => this.RaiseAndSetIfChanged(ref _canRemoveGroup, value, nameof(CanRemoveGroup));
        }
        private bool _canRemoveGroup = false;

        public bool CanEditGroup
        {
            get => _canEditGroup;
            set => this.RaiseAndSetIfChanged(ref _canEditGroup, value, nameof(CanEditGroup));
        }
        private bool _canEditGroup = false;

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => this.RaiseAndSetIfChanged(ref _selectedGroup, value, nameof(SelectedGroup));
        }
        private Group _selectedGroup;

        public void RemoveGroup()
        {
            Groups.Remove(SelectedGroup);
            CanRemoveGroup = false;
            CanEditGroup = false;
        }

        public void AddGroup()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddGroupVm(HostScreen));
        }

        public void EditGroup()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddGroupVm(HostScreen, SelectedGroup));
        }

        public ObservableCollection<string> Lessons
        {
            get;
        } = App.Instance.Lessons;

        public string SelectedLesson
        {
            get => _selectedLesson;
            set => this.RaiseAndSetIfChanged(ref _selectedLesson, value, nameof(SelectedLesson));
        }
        private string _selectedLesson;

        public string InputLesson 
        {
            get => _input;
            set => this.RaiseAndSetIfChanged(ref _input, value, nameof(InputLesson));
        }
        private string _input;

        public bool CanRemoveLesson
        {
            get => _canRemoveLesson;
            set => this.RaiseAndSetIfChanged(ref _canRemoveLesson, value, nameof(CanRemoveGroup));
        }
        private bool _canRemoveLesson = false;

        public void RemoveLesson()
        {
            
            Lessons.Remove(SelectedLesson);
        }

        public void AddLesson()
        {
            if(Lessons.Contains(InputLesson))
            {
                App.ErrorMessageBox("Ошибка", "Уже есть такой урок");
                return;
            }
            if(string.IsNullOrWhiteSpace(InputLesson))
            {
                App.ErrorMessageBox("Безимянный урок!", "Введите имя урока");
                return;
            }
            Lessons.Add(InputLesson.Trim());
        }

        public async void Generate()
        {
            var window = new Windows.GenerationView();
            window.ViewModel = new GenerationVm();
            await window.ShowDialog(App.MainWindowInstance);
        }

    }
}
