using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DailyWeeklyMission.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        //상태(프로퍼티)가 변경되었음을 WPF에 알리는 메서드
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}