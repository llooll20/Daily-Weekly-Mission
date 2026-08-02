using DailyWeeklyMission.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyWeeklyMission.Commands
{
    internal class PreviousMonthCommand : CommandBase
    {
        private MissionDetailViewModel _exViewModel;

        public PreviousMonthCommand(MissionDetailViewModel exViewModel)
        {
            _exViewModel = exViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            _exViewModel.MoveToPreviousMonth();
        }
    }
}
