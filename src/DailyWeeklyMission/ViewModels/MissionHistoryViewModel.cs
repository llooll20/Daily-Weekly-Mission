using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Views;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace DailyWeeklyMission.ViewModels
{
    public class MissionHistoryViewModel : ViewModelBase
    {
        private MissionHistoryWindow _missionHistoryWindow;
        private readonly IMissionRepository _missionRepository;
        private ObservableCollection<Mission> _missions;
        private Mission? _selectedMission;

        public MissionHistoryViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            SetMissions(_missionRepository);
        }

        public void SetMissions(IMissionRepository missionRepository)
        {
            _missions= new ObservableCollection<Mission>(missionRepository.GetAllMissions());
            OnPropertyChanged(nameof(Missions));
        }
        public ObservableCollection<Mission> Missions
        {
            get => _missions;
            private set
            {
                _missions = value;
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
