using DailyWeeklyMission;
using DailyWeeklyMission.Models;
using DailyWeeklyMission.ViewModels;
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

    // 생성자: 초기 미션 타입을 받아서 UI를 설정
    public AddMissionWindow()
    {
        InitializeComponent();
        
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => DragMove();

    private void BtnClose_Click(object sender, RoutedEventArgs e)   => NavigateBack();
    private void BtnCancel_Click(object sender, RoutedEventArgs e)  => NavigateBack();


    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        NavigateBack();
    }


    private void NavigateBack()
    {
        DialogResult = true;
        Close();
    }

}
