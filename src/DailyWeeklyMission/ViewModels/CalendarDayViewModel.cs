using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.ViewModels
{
    // 각 날짜를 나타내는 뷰모델
    public class CalendarDayViewModel : ViewModelBase
    {
        public DateTime Date { get; }

        public int Day => Date.Day;

        public bool IsCurrentMonth { get; }

        public bool IsCompleted { get; }

        public CalendarDayViewModel(
            DateTime date,
            bool isCurrentMonth,
            bool isCompleted)
        {
            Date = date;
            IsCurrentMonth = isCurrentMonth;
            IsCompleted = isCompleted;
        }
    }
}
