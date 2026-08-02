using System;

namespace DailyWeeklyMission.Models
{
    public class MissionCompletionRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MissionId { get; set; }
        public DateOnly CompletedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly PeriodKey { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int CompletedCount { get; set; } = 1;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public bool IsInPeriod(DateOnly periodKey) => PeriodKey == periodKey;
    }
}
