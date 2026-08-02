using System.Windows;
using System.Windows.Input;

namespace DailyWeeklyMission;

public partial class AddMissionWindow : Window
{
    public AddMissionWindow(MissionType initialType = MissionType.Daily)
    {
        InitializeComponent();
        if (initialType == MissionType.WeeklyCount || initialType == MissionType.WeeklyDays)
        {
            BtnTypeDaily.IsChecked   = false;
            BtnTypeWeekly.IsChecked  = true;
            WeekdayPanel.Visibility  = Visibility.Visible;
        }
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void BtnClose_Click(object sender, RoutedEventArgs e)   => NavigateBack();
    private void BtnCancel_Click(object sender, RoutedEventArgs e)  => NavigateBack();

    private void BtnTypeDaily_Click(object sender, RoutedEventArgs e)
    {
        BtnTypeDaily.IsChecked  = true;
        BtnTypeWeekly.IsChecked = false;
        WeekdayPanel.Visibility = Visibility.Collapsed;
    }

    private void BtnTypeWeekly_Click(object sender, RoutedEventArgs e)
    {
        BtnTypeWeekly.IsChecked = true;
        BtnTypeDaily.IsChecked  = false;
        WeekdayPanel.Visibility = Visibility.Visible;
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        // TODO: 유효성 검사 및 데이터 저장 로직 연결
        NavigateBack();
    }

    private void NavigateBack()
    {
        new MainWindow().Show();
        Close();
    }
}
