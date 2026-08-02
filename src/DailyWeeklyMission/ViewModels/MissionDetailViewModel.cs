using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace DailyWeeklyMission.ViewModels
{
    public class MissionDetailViewModel : ViewModelBase
    {
        private string _title;
        private string _isCompleted;
        private string _missionType;
        private string _StartDate;
        private string _EndDate;
        private Mission _mission;
        public string _progressText;
        public string _progressPercentage;

        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public MissionDetailViewModel(Mission mission)
        {
            _mission = mission;
            PreviousMonthCommand = new PreviousMonthCommand(this);
            NextMonthCommand = new NextMonthCommand(this);

            _displayMonth = mission.StartDate ?? DateTime.Today;   // 또는 DateTime.Today

            BuildCalendar();
        }
        public string IsCompleted
        {
            get 
            {
                if(_mission.IsActive==true)
                    return "In Progress";
                else
                    return "Closed";
            }
            set
            {
                _isCompleted = value;
                OnPropertyChanged();
            }
        }
        public string MissionType
        {
            get 
            { 
                if(_mission.Type == 0)
                    return "Daily Mission";
                else
                    return "Weekly Mission";
            }
            set
            {
                _missionType = value;
                OnPropertyChanged();
            }
        }
        
        public string Title
        {
            get => _mission.Title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string StartDate
        {
            get => _mission.StartDate.HasValue ? _mission.StartDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            set
            {
                _StartDate = value;
                OnPropertyChanged();
            }
        }

        public string EndDate
        {
            get => _mission.EndDate.HasValue ? _mission.EndDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            set
            {
                _EndDate = value;
                OnPropertyChanged();
            }
        }
        public string ProgressText
        {
            get => $"{_mission.CurrentCount}/{_mission.TargetCount}";
            set
            {
                _progressText = value;
                OnPropertyChanged();
            }
        }

        // 진행률을 계산하여 반환
        public double ProgressValue =>
            _mission.TargetCount == 0
                ? 0
                : (double)_mission.CurrentCount / _mission.TargetCount * 100;

        // 진행률을 퍼센트로 표시
        public string ProgressPercentage =>
            $"{ProgressValue:F0}%";

        // 달력의 날짜를 나타내는 CalendarDayViewModel 객체들의 컬렉션
        public ObservableCollection<CalendarDayViewModel> CalendarDays
        {
            get;
        }
            = new();
        public DateTime _displayMonth = DateTime.Today;

        // 현재 표시되는 달을 "yyyy.MM" 형식으로 반환
        public string CurrentMonth
        {
            get => _displayMonth.ToString("yyyy.MM");
        }

        // 달력 생성
        public void BuildCalendar()
        {
            CalendarDays.Clear();

            DateTime firstDay = new DateTime(
                _displayMonth.Year,
                _displayMonth.Month,
                1);

            int offset = (int)firstDay.DayOfWeek;

            DateTime startDate = firstDay.AddDays(-offset);

            for (int i = 0; i < 42; i++)
            {
                DateTime day = startDate.AddDays(i);

                bool completed =
                    _mission.CompletedDates.Any(d => d.Date == day);

                CalendarDays.Add(
                    new CalendarDayViewModel(
                        day,
                        day.Month == _displayMonth.Month,
                        completed));
            }
        }

        // 이전 달로 이동
        public void MoveToPreviousMonth()
        {
            _displayMonth = _displayMonth.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonth));
            BuildCalendar();
        }
        // 다음 달로 이동
        public void MoveToNextMonth()
        {
            _displayMonth = _displayMonth.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonth));
            BuildCalendar();
        }

        // 달력에서 완료된 날짜 수를 계산
        public int CalendarDaysCompletedCount
        {
            get => _mission.CompletedDates.Count;
        }
    }
}
