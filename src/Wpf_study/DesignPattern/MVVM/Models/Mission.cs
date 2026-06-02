using System;
using System.Collections.Generic;

namespace Wpf_study.DesignPattern.MVVM.Models
{
    public class Mission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public MissionType Type { get; set; }
        public int TargetCount { get; set; } = 1;
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public List<DayOfWeek> ScheduledDays { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public bool IsActive { get; set; } = true;

        public bool IsWeekly() => Type == MissionType.Weekly;

        public bool HasScheduleOn(DayOfWeek dayOfWeek) => ScheduledDays.Contains(dayOfWeek);
    }
}
