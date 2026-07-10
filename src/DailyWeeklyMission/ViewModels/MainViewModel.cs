using DailyWeeklyMission.Commands;
using DailyWeeklyMission.Repositories;
using System.Windows.Input;

namespace DailyWeeklyMission.ViewModels
{
    public class MainViewModel
    {
        private readonly IMissionRepository _missionRepository;

        public ICommand OpenAddMissionCommand { get; set; }
    }
}
