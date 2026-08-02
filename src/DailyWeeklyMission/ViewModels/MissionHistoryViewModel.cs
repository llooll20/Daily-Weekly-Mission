using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Views;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Input;

namespace DailyWeeklyMission.ViewModels
{
    public class MissionHistoryViewModel : ViewModelBase
    {
        private readonly IMissionRepository _missionRepository;
        private MissionDetailViewModel? _selectedMissionDetail;

        private ObservableCollection<Mission> _missions;
        public Mission? _selectedMission;

        public ICommand RestoreMissionCommand { get; set; }
        public ICommand DeleteMissionCommand { get; set; }

        public MissionHistoryViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            SetMissions(_missionRepository);

            RestoreMissionCommand = new RestoreMissionCommand(_missionRepository,this);
            DeleteMissionCommand = new DeleteMissionCommand(_missionRepository, this);
        }

        // 저장소의 전체미션 목록을 가져와서 Missions 속성에 설정
        public void SetMissions(IMissionRepository missionRepository)
        {
            _missions= new ObservableCollection<Mission>(missionRepository.GetAllMissions());
            OnPropertyChanged(nameof(Missions));
        }

        // 전체 미션 목록
        public ObservableCollection<Mission> Missions
        {
            get => _missions;
            private set
            {
                _missions = value;
                OnPropertyChanged();
            }
        }

        // 선택된 미션
        public Mission? SelectedMission
        {
            get => _selectedMission;
            set
            {
                _selectedMission = value;
                OnPropertyChanged();
                if (value != null)
                {
                    // 선택된 미션이 변경될 때, 해당 미션의 상세 정보를 MissionDetailViewModel로 생성하여 SelectedMissionDetail에 설정
                    SelectedMissionDetail = new MissionDetailViewModel(value);
                }
                else
                {
                    SelectedMissionDetail = null;
                }
            }
        }

        // 선택된 미션의 상세 정보
        public MissionDetailViewModel? SelectedMissionDetail
        {
            get => _selectedMissionDetail;
            set
            {
                _selectedMissionDetail = value;
                OnPropertyChanged();
            }
        }

        public void RefreshMissions()
        {
            SetMissions(_missionRepository);
        }
    }

}
