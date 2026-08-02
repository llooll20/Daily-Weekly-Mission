using System;

namespace DailyWeeklyMission.Services.Time
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateOnly Today { get; }
    }
}
