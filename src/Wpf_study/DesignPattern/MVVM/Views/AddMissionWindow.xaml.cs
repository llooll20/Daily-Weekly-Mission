using System.Windows;

namespace Wpf_study.DesignPattern.MVVM.Views
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
