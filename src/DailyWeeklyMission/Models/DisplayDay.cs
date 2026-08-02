using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.Models
{
    public class DisplayDay
    {
        public DayOfWeek Day { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsSelected { get; set; }
    }
}
