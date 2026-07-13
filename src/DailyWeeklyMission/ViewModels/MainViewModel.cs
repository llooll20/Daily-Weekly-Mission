using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DailyWeeklyMission.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMissionRepository _missionRepository;
        private ObservableCollection<Mission> _weeklyMissions;
        public ViewMode _viewMode = ViewMode.Nomal_mode;

        public ICommand DeleteMissionCommand { get; set; }

        public ICommand MissionClickCommand { get; set; }

        public MainViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            DeleteMissionCommand = new DeleteMissionCommand(_missionRepository, this);
            MissionClickCommand = new MissionClickCommand(_missionRepository, this);

            SetWeeklyMissions(new ObservableCollection<Mission>(_missionRepository.GetAllMissions()));
        }


        public void RefreshWeeklyMissions()
        {
            WeeklyMissions =
                new ObservableCollection<Mission>(_missionRepository.GetAllMissions());
        }
        private void SetWeeklyMissions(ObservableCollection<Mission> weeklyMissions)
        {
            _weeklyMissions = weeklyMissions;
            OnPropertyChanged(nameof(WeeklyMissions));
        }
        public ObservableCollection<Mission> WeeklyMissions
        {
            get => _weeklyMissions;
            private set
            {
                _weeklyMissions = value;
                OnPropertyChanged();
            }
        }
        public void ChangeViewMode()
        {
            if (_viewMode == ViewMode.Nomal_mode)
            {
                _viewMode = ViewMode.Delete_mode;

            }
            else
            {
                _viewMode = ViewMode.Nomal_mode;
            }
        }
    }
    public enum ViewMode{
        Nomal_mode,
        Delete_mode
    }

}
