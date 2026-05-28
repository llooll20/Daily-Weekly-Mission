using System;

namespace Wpf_study.DesignPattern.MVVM.Services.Time
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateOnly Today { get; }
    }
}
