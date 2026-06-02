using System.Windows;

namespace DailyWeeklyMission
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
