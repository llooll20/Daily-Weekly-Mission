using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DailyWeeklyMission.ViewModels;
using DailyWeeklyMission.Repositories;
namespace DailyWeeklyMission.Views;

public partial class MainWindow : Window
{
    // deleteMode basically off
    private bool _deleteMode = false;
    private MainViewModel _mainViewModel => (MainViewModel)DataContext;

    public MainWindow()
    {

        InitializeComponent();
        var repository = new MissionRepository();
        DataContext = new MainViewModel(repository);
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void BtnClose_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void BtnHistory_Click(object sender, RoutedEventArgs e)
    {
        var missionHistoryWindow =new MissionHistoryWindow();
        var repository = new MissionRepository();
        var historyViewModel = new MissionHistoryViewModel(repository);

        missionHistoryWindow.DataContext = historyViewModel;
        missionHistoryWindow.ShowDialog();
        Close();
    }

    private void BtnDeleteMode_Click(object sender, RoutedEventArgs e)
    {
        _deleteMode = !_deleteMode;
        DeleteModeBanner.Visibility = _deleteMode ? Visibility.Visible : Visibility.Collapsed;

        var path = (System.Windows.Shapes.Path)BtnDeleteMode.Content;
        path.Stroke = _deleteMode
            ? (Brush)FindResource("Red500")
            : (Brush)FindResource("Gray700");
    }

    private void BtnAddWeekly_Click(object sender, RoutedEventArgs e)
    {
        var AddMissionWindow = new AddMissionWindow(UiMissionType.Daily);
        var repository = new MissionRepository();
        AddMissionWindow.DataContext = new AddMissionViewModel(repository);
        if (AddMissionWindow.ShowDialog() == true)
        {
            _mainViewModel.RefreshWeeklyMissions();
        }
    }

    private void BtnAddDaily_Click(object sender, RoutedEventArgs e)
    {
        var AddMissionWindow = new AddMissionWindow(UiMissionType.Daily);
        var repository = new MissionRepository();
        AddMissionWindow.DataContext = new AddMissionViewModel(repository);
        if (AddMissionWindow.ShowDialog() == true)
        {
            _mainViewModel.RefreshWeeklyMissions();
        }
    }
}

// ?? 紐⑤뜽 ??????????????????????????????????????????????????????????????
public enum UiMissionType { Daily, WeeklyCount, WeeklyDays }