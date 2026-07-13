using System;
using System.Collections.Generic;

namespace DailyWeeklyMission.Models
{
    public class Mission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public MissionType Type { get; set; }
        public int TargetCount { get; set; } = 1;
        public int CurrentCount { get; set; } = 0;
        public DateTime? StartDate { get; set; } = DateTime.Today;
        public DateTime? EndDate { get; set; } = DateTime.Today;
        public List<DayOfWeek> ScheduledDays { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public bool IsActive { get; set; } = true;

        public bool IsWeekly() => Type == MissionType.Weekly;

        public bool HasScheduleOn(DayOfWeek dayOfWeek) => ScheduledDays.Contains(dayOfWeek);
    }
}
