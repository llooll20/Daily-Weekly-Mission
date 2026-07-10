using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using DailyWeeklyMission.Models;

namespace DailyWeeklyMission.Commands
{
    public class SaveMissionCommand : CommandBase
    {
        private AddMissionViewModel _addMissionViewModel;
        private IMissionRepository _missionRepository;

        public SaveMissionCommand(AddMissionViewModel addMissionViewModel, IMissionRepository missionRepository)
        {
            this._addMissionViewModel = addMissionViewModel;
            this._missionRepository = missionRepository;
        }
        public Mission CreateMission()
        {
            var mission = new Mission
            {
                Title = _addMissionViewModel.Title,
                Content = _addMissionViewModel.Content,
                TargetCount = _addMissionViewModel.TargetCount,
                StartDate = _addMissionViewModel.StartDate ?? DateTime.Today,
                EndDate = _addMissionViewModel.EndDate ?? DateTime.Today
            };
            return mission;
        }
        public void Save(Mission mission)
        {
            _missionRepository.SaveMission(mission);
        }

        public bool CanSave()
        {
            return true;
           // return !string.IsNullOrWhiteSpace(_addMissionViewModel.MissionName) && _addMissionViewModel.SelectedCategory != null;
        }

        public override void Execute(object? parameter)
        {
            
            Save(CreateMission());
        }
        public override bool CanExecute(object? parameter)
        {
            return CanSave();
        }


    }
}
