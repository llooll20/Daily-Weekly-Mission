using System;

namespace Wpf_study.DesignPattern.MVVM.Services.Time
{
    public class SystemClock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
        public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);
    }
}
