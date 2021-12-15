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
            screen.Router.NavigationStack.Clear();
            HostScreen = screen;
        }

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
            CanEditTeacher = false;
            CanRemoveTeacher = false;
        }

        public void AddGroup()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddGroupVm(HostScreen));
        }

        public void EditGroup()
        {
            HostScreen.Router.Navigate.Execute(new EditOrAddGroupVm(HostScreen, SelectedGroup));
        }
    }
}
