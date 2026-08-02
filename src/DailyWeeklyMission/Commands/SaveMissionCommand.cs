using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using System.Windows;

namespace DailyWeeklyMission.Commands
{
    public class SaveMissionCommand : CommandBase
    {
        private AddMissionViewModel _addMissionViewModel;
        private IMissionRepository _missionRepository;
        private Mission _mission;

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

            _mission = mission;

            return mission;
        }
        public void Save(Mission mission)
        {
            _missionRepository.SaveMission(mission);
        }



        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {

            Save(CreateMission());
        }
    }
}
