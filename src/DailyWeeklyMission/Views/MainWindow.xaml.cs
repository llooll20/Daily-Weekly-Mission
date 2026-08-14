using DailyWeeklyMission.Models;
using DailyWeeklyMission.Repositories;
using DailyWeeklyMission.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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

    //미션 기록 창 생성
    private void BtnHistory_Click(object sender, RoutedEventArgs e)
    {
        var missionHistoryWindow =new MissionHistoryWindow();
        var repository = new MissionRepository();
        var historyViewModel = new MissionHistoryViewModel(repository);

        missionHistoryWindow.DataContext = historyViewModel;
        missionHistoryWindow.ShowDialog();

    }

    //삭제모드 버튼 클릭시, _deleteMode 토글 및 DeleteModeBanner Visibility 변경
    private void BtnDeleteMode_Click(object sender, RoutedEventArgs e)
    {
        _deleteMode = !_deleteMode;
        DeleteModeBanner.Visibility = _deleteMode ? Visibility.Visible : Visibility.Collapsed;

        var path = (System.Windows.Shapes.Path)BtnDeleteMode.Content;
        path.Stroke = _deleteMode
            ? (Brush)FindResource("Red500")
            : (Brush)FindResource("Gray700");
    }

    //주간미션 추가 버튼 클릭시, AddMissionWindow 생성 및 DataContext 설정 후 ShowDialog() 호출
    private void BtnAddWeekly_Click(object sender, RoutedEventArgs e)
    {
        var AddMissionWindow = new AddMissionWindow();
        var repository = new MissionRepository();
        AddMissionWindow.DataContext = new AddMissionViewModel(repository, MissionType.Weekly);
        if (AddMissionWindow.ShowDialog() == true)
        {
            _mainViewModel.RefreshMissions();
        }
    }

    //일간미션 추가 버튼 클릭시, AddMissionWindow 생성 및 DataContext 설정 후 ShowDialog() 호출
    private void BtnAddDaily_Click(object sender, RoutedEventArgs e)
    {
        var AddMissionWindow = new AddMissionWindow();
        var repository = new MissionRepository();
        AddMissionWindow.DataContext = new AddMissionViewModel(repository, MissionType.Daily);
        if (AddMissionWindow.ShowDialog() == true)
        {
            _mainViewModel.RefreshMissions();
        }
    }
}