using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Views;
using System.Collections.ObjectModel;

namespace DailyWeeklyMission.ViewModels
{
    public class MissionHistoryViewModel : ViewModelBase
    {
        private MissionHistoryWindow _missionHistoryWindow;
        private readonly IMissionRepository _missionRepository;
        private ObservableCollection<Mission> Missions;
        private Mission? _selectedMission;

        public MissionHistoryViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public ObservableCollection<Mission> WeeklyMissions
        {
            get => Missions;
            private set
            {
                Missions = value;
                OnPropertyChanged();
            }
        }
        public Mission? SelectedMission
        {
            get => _selectedMission;
            set
            {
                _selectedMission = value;
                OnPropertyChanged();
            }
        }
    }

}
