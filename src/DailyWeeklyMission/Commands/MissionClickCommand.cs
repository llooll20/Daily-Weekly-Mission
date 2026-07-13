using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.Commands
{
    internal class MissionClickCommand : CommandBase
    {
        private IMissionRepository _missionRepository;
        private MainViewModel _mainViewModel;
        private Mission _mission;

        public MissionClickCommand(IMissionRepository missionRepository, MainViewModel mainViewModel)
        {
            _missionRepository = missionRepository;
            _mainViewModel = mainViewModel;
        }

        public void MissionClick(Mission mission)
        {
            if (mission == null)
            {
                return;
            }
            if( _mainViewModel._viewMode == ViewMode.Delete_mode )
            {
                _missionRepository.DeleteMission(mission.Id);
            }
            else
            {
                _missionRepository.IncrementCurrentCount(mission);
            }
            _mainViewModel.RefreshWeeklyMissions();
        }

        public void DeleteMission(Mission mission)
        {
            _missionRepository.DeleteMission(mission.Id);
        }
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            MissionClick((Mission)parameter);
        }
    }
}

