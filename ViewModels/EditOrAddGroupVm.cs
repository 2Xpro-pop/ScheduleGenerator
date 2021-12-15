using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleGenerator.ViewModels
{
    public class EditOrAddGroupVm : ViewModelBase, IRoutableViewModel
    {
        private Group? _groupEdit;
        public EditOrAddGroupVm(IScreen screen, Models.Group group = null)
        {
            _groupEdit = group;
            if(_groupEdit != null)
            {
                Name = _groupEdit.Name;
                if(_groupEdit.NeedLessons != null)
                {
                    foreach(var kv in _groupEdit.NeedLessons)
                    {
                        for(int i =0; i < NeedLessons.Count; i++)
                        {
                            if(NeedLessons[i].Key == kv.Key)
                            {
                                NeedLessons[i].Value = kv.Value;
                                break;
                            }
                        }
                    }
                }
                if(_groupEdit.BadClock != null)
                {
                    foreach(var bad in _groupEdit.BadClock)
                    {
                        BadClocks[bad] = true;
                    }
                }
            }

            HostScreen = screen;
        }

        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "EditOrAddGroupVm";

        public string Name 
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value, nameof(Name));
        }
        private string _name;

        public bool[] BadClocks 
        {
            get;
        } = new bool[8];

        public List<EditableKeyValuePair<string,int>> NeedLessons 
        {
            get;
        } = new List<EditableKeyValuePair<string, int>> (
            App.Instance.Lessons.Select(f => new EditableKeyValuePair<string, int>() { Key = f, Value = 0} )
        );

        public void SaveGroup()
        {
            var edit = _groupEdit != null;
            if(!edit)
            {
                _groupEdit = new Group();
            }
            _groupEdit.Name = Name;
            _groupEdit.BadClock = BadClock();
            _groupEdit.NeedLessons = BuildNeedLessons();
            if(!edit)
            {
                App.Instance.Groups.Add(_groupEdit);
            }
            else
            {
                var index = App.Instance.Groups.IndexOf(_groupEdit);
                App.Instance.Groups[index] = _groupEdit;
            }
            HostScreen.Router.NavigateBack.Execute();
        }

        public void Cancel()
        {
            HostScreen.Router.NavigateBack.Execute();
        }

        private ReadOnlyCollection<int> BadClock()
        {
            var badCloks = new List<int>();
            for(int i=0; i<8; i++)
            {
                if(BadClocks[i])
                {
                    badCloks.Add(i);
                }
            }
            return new ReadOnlyCollection<int>(badCloks);
        }

        private Dictionary<string, int> BuildNeedLessons()
        {
            return NeedLessons.ToDictionary( f => f.Key, f => f.Value);
        }

    }
}
