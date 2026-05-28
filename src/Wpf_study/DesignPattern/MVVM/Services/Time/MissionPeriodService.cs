using System;

namespace Wpf_study.DesignPattern.MVVM.Services.Time
{
    public class MissionPeriodService
    {
        private readonly IClock clock;

        public MissionPeriodService(IClock clock)
        {
            this.clock = clock;
        }

        public DateOnly GetDailyKey()
        {
            return clock.Today;
        }

        public DateOnly GetWeeklyKey()
        {
            DateOnly today = clock.Today;
            int diff = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return today.AddDays(-diff);
        }
    }
}
