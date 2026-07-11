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

        public ICommand CompleteMissionCommand;
        public ICommand DeleteMissionCommand;

        public MainViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
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
    }

    

}
