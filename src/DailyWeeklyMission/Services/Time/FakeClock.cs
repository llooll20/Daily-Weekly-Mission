using System;

namespace DailyWeeklyMission.Services.Time
{
    public class FakeClock : IClock
    {
        public DateTimeOffset Now { get; set; }

        public DateOnly Today => DateOnly.FromDateTime(Now.DateTime);

        public void SetNow(DateTimeOffset now)
        {
            Now = now;
        }
    }
}
