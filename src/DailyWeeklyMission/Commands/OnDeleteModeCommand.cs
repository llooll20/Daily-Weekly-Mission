using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;

namespace DailyWeeklyMission.Commands
{
    public class OnDeleteModeCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        public OnDeleteModeCommand(IMissionRepository repository, MainViewModel mainmodel) 
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
