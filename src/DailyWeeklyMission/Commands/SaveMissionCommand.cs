using DailyWeeklyMission.Models;
using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace DailyWeeklyMission.Commands
{
    public class SaveMissionCommand : CommandBase
    {
        private AddMissionViewModel _addMissionViewModel;
        private IMissionRepository _missionRepository;
        private Mission _mission;
        private MissionType _type;

        public SaveMissionCommand(AddMissionViewModel addMissionViewModel, IMissionRepository missionRepository, MissionType type)
        {
            this._addMissionViewModel = addMissionViewModel;
            this._missionRepository = missionRepository;
            this._type = type;
        }
        public Mission CreateMission()
        {

            var mission = new Mission
            {
                Title = _addMissionViewModel.Title,
                Content = _addMissionViewModel.Content,
                Type = _type,
                StartDate = _addMissionViewModel.StartDate ?? DateTime.Today,
                EndDate = _addMissionViewModel.EndDate ?? DateTime.Today,
                ScheduledDays = _addMissionViewModel.Days
                    .Where(d => d.IsSelected)
                    .Select(d => d.Day)
                    .ToList(),
                WeeklyMode = _addMissionViewModel.SelectedMode,
                TargetCount = _addMissionViewModel.TargetCount
            };

            if(mission.Type == MissionType.Weekly)
            {
                mission.TargetCount = _addMissionViewModel.SelectedMode == WeeklyMissionMode.Days
                    ? _addMissionViewModel.Days.Count(d => d.IsSelected)
                    : _addMissionViewModel.TargetCount;
            }

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
