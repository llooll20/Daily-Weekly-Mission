using System;

namespace Wpf_study.DesignPattern.MVVM.Services.Time
{
    public class FakeClock : IClock
    {
        public DateTimeOffset Now { get; set; }

        public DateOnly Today => DateOnly.FromDateTime(Now.DateTime);
    }
}
