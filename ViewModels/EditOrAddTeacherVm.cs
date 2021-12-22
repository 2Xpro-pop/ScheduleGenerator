using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ScheduleGenerator.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleGenerator.ViewModels
{
    public class EditOrAddTeacherVm: ViewModelBase, IRoutableViewModel
    {

        private Teacher? _teacherEdit;

        public EditOrAddTeacherVm(IScreen screen, Teacher? teacherEdit=null)
        {
            _teacherEdit = teacherEdit;
            HostScreen = screen;
            if(_teacherEdit == null)
            {
                BadTimes = new Week<bool>[8];
                for(int i = 0; i < 8; i++)
                {
                    BadTimes[i] = new Week<bool>();
                }
                SelectedLesson = App.Instance.Lessons[0];
            }
            else
            {
                BadTimes = SetBadTimes();
                SelectedLesson = string.IsNullOrEmpty(_teacherEdit.Lesson) ? App.Instance.Lessons[0] : _teacherEdit.Lesson;
                Name = _teacherEdit.Name;
            }
            
        }

        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "EditOrAddTeacherVm";
        public Week<bool>[] BadTimes { get; set;}
        public IEnumerable<string> Lessons {get;} = App.Instance.Lessons;
        public string Name 
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value, nameof(Name));
        }
        private string _name;

        public string SelectedLesson 
        {
            get => _selectedLesson;
            set => this.RaiseAndSetIfChanged(ref _selectedLesson, value, nameof(SelectedLesson));
        }
        private string _selectedLesson;
        public void SaveTeacher()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                App.ErrorMessageBox("Безимянный учитель!", "Введите имя учителя");
                return;
            }
            var edit = _teacherEdit != null;
            if(!edit)
            {
                _teacherEdit = new Teacher();
            }
            _teacherEdit.Name = Name.Trim();
            _teacherEdit.BadTimes = GetBadTimes();
            _teacherEdit.Lesson = SelectedLesson;
            if(!edit)
            {
                App.Instance.Teachers.Add(_teacherEdit);
            }
            else
            {
                var index = App.Instance.Teachers.IndexOf(_teacherEdit);
                App.Instance.Teachers[index] = _teacherEdit;
            }
            HostScreen.Router.NavigateBack.Execute();
        }

        public void Cancel()
        {
            HostScreen.Router.NavigateBack.Execute();
        }

        private ReadOnlyCollection<int> GetBadTimes()
        {
            var badTimes = new List<int>();

            badTimes.Add(-1);

            for(int i=0;i < 8; i++)
            {
                if(BadTimes[i].Monday)
                {
                    badTimes.Add(0 + i);
                }
                if(BadTimes[i].Tuesday)
                {
                    badTimes.Add(8 + i);
                }
                if(BadTimes[i].Wednesday)
                {
                    badTimes.Add(16 + i);
                }
                if(BadTimes[i].Thursday)
                {
                    badTimes.Add(24 + i);
                }
                if(BadTimes[i].Friday)
                {
                    badTimes.Add(32 + i);
                }
                if(BadTimes[i].Saturaday)
                {
                    badTimes.Add(40 + i);
                }
            }

            return new ReadOnlyCollection<int>(badTimes);
        }

        private Week<bool>[] SetBadTimes()
        {
            var badTimes = new Week<bool>[8];

            for(int i=0;i < 8; i++)
            {
                badTimes[i] = new Week<bool>();
                if(_teacherEdit.BadTimes == null)
                {
                    continue;
                }
                badTimes[i].Monday = _teacherEdit.BadTimes.Contains(0 + i);
                badTimes[i].Tuesday = _teacherEdit.BadTimes.Contains(8 + i);
                badTimes[i].Wednesday = _teacherEdit.BadTimes.Contains(16 + i);
                badTimes[i].Thursday = _teacherEdit.BadTimes.Contains(24 + i);
                badTimes[i].Friday = _teacherEdit.BadTimes.Contains(32 + i);
                badTimes[i].Saturaday = _teacherEdit.BadTimes.Contains(40 + i);
            }

            return badTimes;
        }
    }
}
