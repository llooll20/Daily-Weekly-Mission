using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using System.Windows.Threading;

namespace DailyWeeklyMission.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMissionRepository _missionRepository;
        private ObservableCollection<Mission> _weeklyMissions;
        private ObservableCollection<Mission> _dailyMissions;

        // DispatcherTimer를 사용하여 매일 자정에 미션 상태를 갱신
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private DateTime _lastDate = DateTime.Today;

        public ViewMode _viewMode = ViewMode.Nomal_mode;

        public ICommand OnDeleteModeCommand { get; set; }

        public ICommand MissionClickCommand { get; set; }

        public MainViewModel(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
            OnDeleteModeCommand = new OnDeleteModeCommand(_missionRepository, this);
            MissionClickCommand = new MissionClickCommand(_missionRepository, this);

            _timer.Interval = TimeSpan.FromMinutes(1); // 1분
            _timer.Tick += OnTimerTick;
            _timer.Start();

            RefreshMissions();
        }

        //저장소에서 모든 미션을 가져와 WeeklyMissions 업데이트.
        public void RefreshMissions()
        {
            SetMissions(new ObservableCollection<Mission>(_missionRepository.GetAllMissions()));
        }
        
        // WeeklyMissions 속성을 업데이트
        private void SetMissions(ObservableCollection<Mission> missions)
        {
            missions.All(mission =>
            {
                CheckMissionPeriod(mission);   //미션 종료 기간 체크
                _missionRepository.RefreshMission(mission); //미션 카운트 갱신
                return true; 
            });
            WeeklyMissions = new ObservableCollection<Mission>(
                missions.Where(m => m.IsActive && m.IsWeekly()));
            DailyMissions = new ObservableCollection<Mission>(
                missions.Where(m => m.IsActive && !m.IsWeekly()));
            OnPropertyChanged(nameof(missions));
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

        public ObservableCollection<Mission> DailyMissions
        {
            get => _dailyMissions;
            private set
            {
                _dailyMissions = value;
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

        //미션 기간이 지났는지 확인하고, 지났다면 IsActive를 false로 설정
        public void CheckMissionPeriod(Mission mission)
        {
            if (mission.EndDate.HasValue &&
                mission.EndDate.Value.Date < DateTime.Today)
            {
                mission.IsActive = false;
            }
        }

        //날짜가 변경되었는지 확인하고, 변경되었다면 미션 상태를 갱신
        private void CheckDateChange()
        {
            DateTime currentDate = DateTime.Today;
            if (currentDate != _lastDate)
            {
                _lastDate = currentDate;
                RefreshMissions();
            }
        }

        // DispatcherTimer의 Tick 이벤트 핸들러
        private void OnTimerTick(object? sender, EventArgs e)
        {
            CheckDateChange();
        }

    }
    public enum ViewMode{
        Nomal_mode,
        Delete_mode
    }

}
