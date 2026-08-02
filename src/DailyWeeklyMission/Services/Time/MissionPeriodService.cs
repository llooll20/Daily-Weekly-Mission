using DailyWeeklyMission.Models;
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

        //"이 날짜가 속한 주의 월요일" 반환
        public DateOnly GetWeeklyKey(DateOnly date)
        {
            int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return date.AddDays(-diff);
        }

        public bool IsDailyMissionDate(DateOnly missionDate)
        {
            return missionDate == GetDailyKey();
        }

        //"missionDate가 현재 주에 속하는가?"
        public bool IsWeeklyMissionDate(DateOnly missionDate)
        {
            return GetWeeklyKey(missionDate) == GetWeeklyKey();
        }

        /// 현재 요일이 미션의 스케줄에 포함되어 있는지 확인
        public bool CanCompleteToday(Mission mission)
        {
            return mission.ScheduledDays.Contains(clock.Today.DayOfWeek);
        }
        public bool IsTodayScheduled(Mission mission)
        {
            return mission.HasScheduleOn(clock.Today.DayOfWeek);
        }
    }
}
