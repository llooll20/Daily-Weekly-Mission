using System.Windows;

namespace DailyWeeklyMission.Views
{
    public partial class AddMissionWindow : Window
    {
        public AddMissionWindow()
        {
            InitializeComponent();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
