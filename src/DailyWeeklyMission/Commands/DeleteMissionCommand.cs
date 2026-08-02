using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;

namespace DailyWeeklyMission.Commands
{
    public class DeleteMissionCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        public DeleteMissionCommand(IMissionRepository repository, MainViewModel mainmodel) 
        {
            _mainViewModel = mainmodel;
        }
        public override bool CanExecute(object? parameter)
        {
            return true;
        }
        public override void Execute(object? parameter)
        {
            _mainViewModel.ChangeViewMode();
        }
    }
}
