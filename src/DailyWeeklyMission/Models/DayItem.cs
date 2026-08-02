using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.Models
{
    public class DayItem
    {
        public DayOfWeek Day { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

        public DayItem(DayOfWeek day, string text)
        {
            Day = day;
            Text = text;
        }
    }
}
