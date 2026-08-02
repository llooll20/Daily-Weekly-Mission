using System;

namespace DailyWeeklyMission.Services.Time
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
            return GetWeeklyKey(clock.Today);
        }

        public DateOnly GetWeeklyKey(DateOnly date)
        {
            int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return date.AddDays(-diff);
        }

        public bool IsDailyMissionDate(DateOnly missionDate)
        {
            return missionDate == GetDailyKey();
        }

        public bool IsWeeklyMissionDate(DateOnly missionDate)
        {
            return GetWeeklyKey(missionDate) == GetWeeklyKey();
        }
    }
}
