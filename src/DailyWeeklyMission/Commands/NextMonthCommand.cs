using DailyWeeklyMission.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;

namespace DailyWeeklyMission.Commands
{
    internal class NextMonthCommand : CommandBase
    {
        private MissionDetailViewModel _exViewModel;

        public NextMonthCommand(MissionDetailViewModel exViewModel)
        {
            _exViewModel = exViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return true;
        }
        public override void Execute(object? parameter)
        {
            _exViewModel.MoveToNextMonth();
        }
    }
}
