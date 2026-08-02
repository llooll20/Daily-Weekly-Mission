using System;

namespace DailyWeeklyMission.Models
{
    public class MissionProgress
    {
        public Guid MissionId { get; set; }
        public int CurrentCount { get; set; }
        public int TargetCount { get; set; }
        public double CompletionRate => TargetCount <= 0 ? 0 : (double)CurrentCount / TargetCount;
        public bool IsCompleted => TargetCount > 0 && CurrentCount >= TargetCount;

        public int GetRemainingCount() => Math.Max(TargetCount - CurrentCount, 0);

        public bool HasPartialProgress() => CurrentCount > 0 && !IsCompleted;
    }
}
