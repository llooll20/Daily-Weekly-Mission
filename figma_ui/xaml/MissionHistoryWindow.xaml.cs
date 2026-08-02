using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DailyWeeklyMission;

public record HistoryItem(
    string Id, string Title, string Type,
    int Current, int Target, int CompletionRate,
    List<string> CompletedDates, string Period,
    string Status);

public partial class MissionHistoryWindow : Window
{
    private readonly List<HistoryItem> _missions = new()
    {
        new("1", "Exercise",          "Weekly", 3, 3, 100, new(){"2026-05-26","2026-05-28","2026-05-30"}, "2026-05-25 ~ 2026-05-31", "completed"),
        new("2", "Read Books",        "Weekly", 4, 5,  80, new(){"2026-05-25","2026-05-27","2026-05-29","2026-05-31"}, "2026-05-25 ~ 2026-05-31", "in-progress"),
        new("3", "Morning Meditation","Daily",  0, 1,   0, new(), "2026-06-01", "not-started"),
    };

    private string _selectedId = "1";
    private DateTime _calendarMonth;

    public MissionHistoryWindow()
    {
        InitializeComponent();
        BuildMissionList();
        SelectMission("1");
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    // ── 목록 ──────────────────────────────────────────────────────────
    private void BuildMissionList()
    {
        MissionList.Children.Clear();
        foreach (var m in _missions)
        {
            var btn = new RadioButton
            {
                Style    = (Style)FindResource("MissionListItemStyle"),
                GroupName = "missions",
                IsChecked = m.Id == _selectedId,
                Tag       = m.Id,
            };
            btn.Content = new StackPanel
            {
                Children =
                {
                    new TextBlock { Text = m.Title, FontSize = 14, FontWeight = FontWeights.Medium, Foreground = (Brush)FindResource("Gray800"), Margin = new(0,0,0,2) },
                    new TextBlock { Text = m.Type,  FontSize = 12, Foreground = (Brush)FindResource("Gray600"), Margin = new(0,0,0,2) },
                    new TextBlock { Text = m.Period,FontSize = 11, Foreground = (Brush)FindResource("Gray500") },
                }
            };
            btn.Checked += (_, _) => SelectMission(m.Id);
            MissionList.Children.Add(btn);
        }
    }

    // ── 상세 ──────────────────────────────────────────────────────────
    private void SelectMission(string id)
    {
        _selectedId = id;
        var m = _missions.First(x => x.Id == id);

        // 캘린더 초기 월 결정
        _calendarMonth = m.CompletedDates.Count > 0
            ? DateTime.Parse(m.CompletedDates[0])
            : DateTime.Parse(m.Period.Split('~')[0].Trim());

        RenderDetail(m);
    }

    private void RenderDetail(HistoryItem m)
    {
        DetailPanel.Children.Clear();

        // 제목 + 상태 배지
        var header = new StackPanel { Spacing = 6 };
        header.Children.Add(new TextBlock
        {
            Text = m.Title, FontSize = 22, FontWeight = FontWeights.SemiBold,
            Foreground = (Brush)FindResource("Gray800")
        });
        var badgeRow = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
        badgeRow.Children.Add(new TextBlock { Text = $"{m.Type} Mission", FontSize = 12, Foreground = (Brush)FindResource("Gray600"), VerticalAlignment = VerticalAlignment.Center });
        badgeRow.Children.Add(MakeBadge(m.Status));
        header.Children.Add(badgeRow);
        DetailPanel.Children.Add(header);

        // 진행률 박스
        var progressBox = new Border
        {
            Background = (Brush)FindResource("Amber100"),
            CornerRadius = new(8), Padding = new(16)
        };
        var progressStack = new StackPanel { Spacing = 8 };
        var progressHeader = new Grid();
        progressHeader.ColumnDefinitions.Add(new ColumnDefinition());
        progressHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        Grid.SetColumn(new TextBlock { Text = "Progress", FontSize = 12, Foreground = (Brush)FindResource("Gray700") }, 0);
        progressHeader.Children.Add(new TextBlock { Text = "Progress", FontSize = 12, Foreground = (Brush)FindResource("Gray700") });
        var countTb = new TextBlock { Text = $"{m.Current} / {m.Target}", FontSize = 16, FontWeight = FontWeights.SemiBold, Foreground = (Brush)FindResource("Gray800"), HorizontalAlignment = HorizontalAlignment.Right };
        Grid.SetColumn(countTb, 1);
        progressHeader.Children.Add(countTb);
        progressStack.Children.Add(progressHeader);
        progressStack.Children.Add(new ProgressBar
        {
            Height = 8, Minimum = 0, Maximum = 100, Value = m.CompletionRate,
            Background = (Brush)FindResource("Amber200"),
            Foreground = (Brush)FindResource("Blue500"),
            BorderThickness = new(0)
        });
        progressStack.Children.Add(new TextBlock
        {
            Text = $"{m.CompletionRate}% Complete",
            FontSize = 12, Foreground = (Brush)FindResource("Gray600"),
            HorizontalAlignment = HorizontalAlignment.Center
        });
        progressBox.Child = progressStack;
        DetailPanel.Children.Add(progressBox);

        // Period
        var periodStack = new StackPanel { Spacing = 6 };
        periodStack.Children.Add(new TextBlock { Text = "Period", FontSize = 12, FontWeight = FontWeights.Medium, Foreground = (Brush)FindResource("Gray700") });
        periodStack.Children.Add(new Border
        {
            Background = (Brush)FindResource("Amber50"),
            BorderBrush = (Brush)FindResource("Amber200"), BorderThickness = new(1),
            CornerRadius = new(6), Padding = new(12, 8),
            Child = new TextBlock { Text = m.Period, FontSize = 13, Foreground = (Brush)FindResource("Gray700") }
        });
        DetailPanel.Children.Add(periodStack);

        // Completed Dates 캘린더
        var calStack = new StackPanel { Spacing = 6 };
        calStack.Children.Add(new TextBlock
        {
            Text = $"Completed Dates ({m.CompletedDates.Count})",
            FontSize = 12, FontWeight = FontWeights.Medium, Foreground = (Brush)FindResource("Gray700")
        });
        calStack.Children.Add(BuildCalendar(m));
        DetailPanel.Children.Add(calStack);
    }

    // ── 캘린더 ────────────────────────────────────────────────────────
    private UIElement BuildCalendar(HistoryItem m)
    {
        var outer = new Border
        {
            Background = (Brush)FindResource("Amber50"),
            BorderBrush = (Brush)FindResource("Amber200"), BorderThickness = new(1),
            CornerRadius = new(8), Padding = new(12)
        };
        var stack = new StackPanel { Spacing = 4 };

        // 월 헤더
        var navGrid = new Grid();
        navGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        navGrid.ColumnDefinitions.Add(new ColumnDefinition());
        navGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var lblMonth = new TextBlock
        {
            Text = _calendarMonth.ToString("MMMM yyyy", CultureInfo.InvariantCulture),
            FontSize = 13, FontWeight = FontWeights.SemiBold,
            Foreground = (Brush)FindResource("Gray800"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetColumn(lblMonth, 1);

        var btnPrev = new Button { Content = "‹", Style = (Style)FindResource("CalendarNavButtonStyle") };
        var btnNext = new Button { Content = "›", Style = (Style)FindResource("CalendarNavButtonStyle") };
        Grid.SetColumn(btnNext, 2);

        btnPrev.Click += (_, _) => { _calendarMonth = _calendarMonth.AddMonths(-1); RenderDetail(m); };
        btnNext.Click += (_, _) => { _calendarMonth = _calendarMonth.AddMonths(1);  RenderDetail(m); };

        navGrid.Children.Add(btnPrev);
        navGrid.Children.Add(lblMonth);
        navGrid.Children.Add(btnNext);
        stack.Children.Add(navGrid);

        // 요일 헤더
        var dayHeaders = new UniformGrid { Columns = 7 };
        foreach (var d in new[] { "S","M","T","W","T","F","S" })
        {
            dayHeaders.Children.Add(new TextBlock
            {
                Text = d, FontSize = 10, FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)FindResource("Gray400"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new(0, 2)
            });
        }
        stack.Children.Add(dayHeaders);

        // 날짜 그리드
        var firstDay = new DateTime(_calendarMonth.Year, _calendarMonth.Month, 1);
        var startDate = firstDay.AddDays(-(int)firstDay.DayOfWeek);
        var lastDay = new DateTime(_calendarMonth.Year, _calendarMonth.Month,
            DateTime.DaysInMonth(_calendarMonth.Year, _calendarMonth.Month));
        var endDate = lastDay.AddDays(6 - (int)lastDay.DayOfWeek);

        var daysGrid = new UniformGrid { Columns = 7 };
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var isCompleted = m.CompletedDates.Contains(date.ToString("yyyy-MM-dd"));
            var inMonth = date.Month == _calendarMonth.Month;

            var cell = new Border
            {
                Width = 28, Height = 28,
                CornerRadius = new(14),
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = isCompleted ? (Brush)FindResource("Green500") : Brushes.Transparent,
                Margin = new(1)
            };
            cell.Child = new TextBlock
            {
                Text = date.Day.ToString(),
                FontSize = 11,
                FontWeight = isCompleted ? FontWeights.Bold : FontWeights.Normal,
                Foreground = isCompleted
                    ? Brushes.White
                    : inMonth
                        ? (Brush)FindResource("Gray700")
                        : (Brush)FindResource("Gray300"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            daysGrid.Children.Add(cell);
        }
        stack.Children.Add(daysGrid);

        if (m.CompletedDates.Count == 0)
        {
            stack.Children.Add(new TextBlock
            {
                Text = "No completed dates yet",
                FontSize = 11, FontStyle = FontStyles.Italic,
                Foreground = (Brush)FindResource("Gray400"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new(0, 4, 0, 0)
            });
        }

        outer.Child = stack;
        return outer;
    }

    // ── 상태 배지 ──────────────────────────────────────────────────────
    private static UIElement MakeBadge(string status)
    {
        var (fg, bg, label) = status switch
        {
            "completed"   => ("#16A34A", "#DCFCE7", "Completed"),
            "in-progress" => ("#2563EB", "#DBEAFE", "In Progress"),
            _             => ("#4B5563", "#F3F4F6", "Not Started"),
        };
        return new Border
        {
            Background = (Brush)new BrushConverter().ConvertFrom(bg)!,
            CornerRadius = new(10), Padding = new(8, 3),
            Child = new TextBlock
            {
                Text = label, FontSize = 11, FontWeight = FontWeights.Medium,
                Foreground = (Brush)new BrushConverter().ConvertFrom(fg)!
            }
        };
    }
}
