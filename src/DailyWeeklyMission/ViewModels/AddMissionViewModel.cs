using System.Windows.Input;
using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Models;

namespace DailyWeeklyMission.ViewModels
{
    public class AddMissionViewModel : ViewModelBase
    {
        private readonly IMissionRepository _missionRepository;

        private string _title = "";
        private string _content = "";
        private int _targetCount = 1;
        private DateTime? _startDate = DateTime.Today;
        private DateTime? _endDate = DateTime.Today;


        public ICommand SaveMissionCommand { get; set; }

        public AddMissionViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            SaveMissionCommand = new SaveMissionCommand(this, _missionRepository);
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

    }
}
