using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using DailyWeeklyMission.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.Commands
{
    class RestoreMissionCommand : CommandBase
    {
        private IMissionRepository _missionRepository;
        private MissionHistoryViewModel _viewModel;

        public RestoreMissionCommand(IMissionRepository missionRepository, MissionHistoryViewModel viewModel)
        {
            _missionRepository = missionRepository;
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            Restore(_viewModel._selectedMission);
            _viewModel.RefreshMissions();
        }

        public void Restore(Mission mission)
        {
            if(mission==null)
            {
                return;
            }
            _missionRepository.ActivateMission(mission);
            mission.CurrentCount = 0;

        }
    }
}
