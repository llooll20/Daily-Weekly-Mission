using DailyWeeklyMission.Properties;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using DailyWeeklyMission.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DailyWeeklyMission
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //실행시 메인 윈도우를 생성하고, MainViewModel을 DataContext로 설정
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var missionRepository = new MissionRepository();
            var mainWindow = new MainWindow();
            mainWindow.DataContext = new MainViewModel(missionRepository);
            mainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            var settings = new Settings();
            MessageBox.Show(settings.MissionsJson);

            base.OnExit(e);
        }
    }
}
