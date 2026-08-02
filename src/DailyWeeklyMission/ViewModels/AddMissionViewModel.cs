using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DailyWeeklyMission.ViewModels
{
    public class AddMissionViewModel : ViewModelBase
    {
        private readonly IMissionRepository _missionRepository;

        private string _title = "";
        private string _content = "";
        private int _targetCount ;
        private DateTime? _startDate = DateTime.Today;
        private DateTime? _endDate = DateTime.Today;

        public MissionType _missionType { get; set; }

        //요일 데이터 
        public ObservableCollection<DayItem> Days { get; }      

        //선택된 모드 (요일 모드 또는 횟수 모드)
        public WeeklyMissionMode _selectedMode { get; set; } = WeeklyMissionMode.Days;

        public ICommand SaveMissionCommand { get; set; }

        public AddMissionViewModel(IMissionRepository missionRepository, MissionType type)
        {
            _missionRepository = missionRepository;
            _missionType= type;
            Days = new ObservableCollection<DayItem>
            {
                new (DayOfWeek.Monday, "Mon"),
                new (DayOfWeek.Tuesday, "Tue"),
                new (DayOfWeek.Wednesday, "Wed"),
                new (DayOfWeek.Thursday, "Thu"),
                new (DayOfWeek.Friday, "Fri"),
                new (DayOfWeek.Saturday, "Sat"),
                new (DayOfWeek.Sunday, "Sun")
            };
            SaveMissionCommand = new SaveMissionCommand(this, _missionRepository, type);
        }

        public string Title { get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public int TargetCount
        {
            get => _targetCount;
            set
            {
                _targetCount = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public WeeklyMissionMode SelectedMode
        {
            get => _selectedMode;
            set
            {
                if (_selectedMode == value)
                    return;

                _selectedMode = value;

                OnPropertyChanged(nameof(SelectedMode));
                OnPropertyChanged(nameof(IsDayMode));
                OnPropertyChanged(nameof(IsCountMode));
                OnPropertyChanged(nameof(IsWeeklyMission));
                OnPropertyChanged(nameof(ShowTargetCount));
            }
        }

        public bool IsDayMode
        {
            get => SelectedMode == WeeklyMissionMode.Days;
            set
            {
                if (value)
                    SelectedMode = WeeklyMissionMode.Days;
            }
        }

        public bool IsCountMode
        {
            get => SelectedMode == WeeklyMissionMode.Count;
            set
            {
                if (value)
                {
                    SelectedMode = WeeklyMissionMode.Count;
                }
            }
        }

        public bool IsWeeklyMission
        {
            get => _missionType == MissionType.Weekly;
        }

        public bool ShowTargetCount
        {
            get
            {
                return _missionType == MissionType.Daily
                    || (_missionType == MissionType.Weekly
                        && SelectedMode == WeeklyMissionMode.Count);
            }
        }
    }
}
