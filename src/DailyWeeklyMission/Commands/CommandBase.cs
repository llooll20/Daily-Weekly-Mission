using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DailyWeeklyMission.Commands
{
    public abstract class CommandBase : ICommand
    {
        // ICommand 인터페이스의 CanExecuteChanged 이벤트를 구현
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // 버튼 활성화, 비활성화 여부를 결정하는 메서드
        public abstract bool CanExecute(object? parameter);

        // 실행할 명령을 정의하는 메서드
        public abstract void Execute(object? parameter);

    }
}
