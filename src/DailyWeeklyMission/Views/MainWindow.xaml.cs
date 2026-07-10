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

    // display Missions data
    public ObservableCollection<MissionItem> WeeklyMissions { get; } = new()
    {
        new() { Id = "w1", Title = "Exercise",    Content = "Workout at the gym",    Current = 1, Target = 3, Completed = false, Type = UiMissionType.WeeklyCount },
        new() { Id = "w2", Title = "Cleaning",    Content = "Clean the house",        Current = 1, Target = 2, Completed = false, Type = UiMissionType.WeeklyDays, ScheduledDays = new(){1,4}, CurrentDay = 2 },
        new() { Id = "w3", Title = "Read Books",  Content = "Read for 30 minutes",    Current = 5, Target = 5, Completed = true,  Type = UiMissionType.WeeklyCount },
    };

    public ObservableCollection<MissionItem> DailyMissions { get; } = new()
    {
        new() { Id = "d1", Title = "Drink Water", Content = "8 glasses of water",     Current = 5, Target = 8, Completed = false, Type = UiMissionType.Daily },
        new() { Id = "d2", Title = "Study",       Content = "Learn new skills",        Current = 2, Target = 2, Completed = true,  Type = UiMissionType.Daily },
    };

    public MainWindow()
    {
        InitializeComponent();
        WeeklyMissionsList.ItemsSource = WeeklyMissions;
        DailyMissionsList.ItemsSource  = DailyMissions;

    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void BtnClose_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void BtnHistory_Click(object sender, RoutedEventArgs e)
    {
        new MissionHistoryWindow().Show();
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
        new AddMissionWindow(UiMissionType.WeeklyCount).Show();
        Close();
    }

    private void BtnAddDaily_Click(object sender, RoutedEventArgs e)
    {
        new AddMissionWindow(UiMissionType.Daily).Show();
        Close();
    }
}

// ?? 紐⑤뜽 ??????????????????????????????????????????????????????????????
public enum UiMissionType { Daily, WeeklyCount, WeeklyDays }

public class MissionItem : INotifyPropertyChanged
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int Current { get; set; }
    public int Target { get; set; }
    public bool Completed { get; set; }
    public UiMissionType Type { get; set; }
    public List<int> ScheduledDays { get; set; } = new();
    public int CurrentDay { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
