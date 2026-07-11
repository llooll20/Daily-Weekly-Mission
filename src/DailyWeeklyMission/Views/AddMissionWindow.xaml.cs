using DailyWeeklyMission;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DailyWeeklyMission.Views;

public partial class AddMissionWindow : Window
{
    private void TxtContent_TextChanged(object sender, TextChangedEventArgs e)
    {
        BindingExpression be = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
        be.UpdateSource();
    }
    public AddMissionWindow(UiMissionType initialType = UiMissionType.Daily)
    {
        InitializeComponent();
        if (initialType == UiMissionType.WeeklyCount || initialType == UiMissionType.WeeklyDays)
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
        // UI-only placeholder: validation and repository saving will be connected later.
        NavigateBack();
    }

    private void NavigateBack()
    {
        DialogResult = true;
        Close();
    }

}
