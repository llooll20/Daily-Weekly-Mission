using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public DateTime LastProgressDate { get; set; }  //마지막 미션 처리일
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public bool IsActive { get; set; } = true;  // 미션이 활성화 상태인지 여부

        public WeeklyMissionMode WeeklyMode { get; set; } = WeeklyMissionMode.Days;

        public bool IsCompleted { get; set; } = false;
        public bool IsWeekly() => Type == MissionType.Weekly;

        public bool HasScheduleOn(DayOfWeek dayOfWeek) => ScheduledDays.Contains(dayOfWeek);

        //완료날짜를 저장하는 리스트
        public List<DateTime> CompletedDates { get; set; }
        = new();

        // DisplayDays 속성은 ScheduledDays를 기반으로 요일을 표시하는 IEnumerable<DisplayDay>를 반환
        
        public bool IsWeeklyCount =>
            Type == MissionType.Weekly &&
            WeeklyMode == WeeklyMissionMode.Count;

        public bool IsWeeklyDays =>
            Type == MissionType.Weekly &&
            WeeklyMode == WeeklyMissionMode.Days;
        public IEnumerable<DisplayDay> DisplayDays =>
        new[]
        {
            new DisplayDay{ Text="M", IsSelected=ScheduledDays.Contains(DayOfWeek.Monday)},
            new DisplayDay{ Text="T", IsSelected=ScheduledDays.Contains(DayOfWeek.Tuesday)},
            new DisplayDay{ Text="W", IsSelected=ScheduledDays.Contains(DayOfWeek.Wednesday)},
            new DisplayDay{ Text="T", IsSelected=ScheduledDays.Contains(DayOfWeek.Thursday)},
            new DisplayDay{ Text="F", IsSelected=ScheduledDays.Contains(DayOfWeek.Friday)},
            new DisplayDay{ Text="S", IsSelected=ScheduledDays.Contains(DayOfWeek.Saturday)},
            new DisplayDay{ Text="S", IsSelected=ScheduledDays.Contains(DayOfWeek.Sunday)}
        };
    }
}
