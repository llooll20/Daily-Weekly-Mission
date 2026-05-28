using System.Windows;

namespace Wpf_study
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void MVVMBtnClick(object sender, RoutedEventArgs e)
        {
            var mainView = new DesignPattern.MVVM.Views.MainView();

            mainView.Show();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
