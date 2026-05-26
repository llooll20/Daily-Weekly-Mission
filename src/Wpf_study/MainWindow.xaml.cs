using System.Windows;
using Wpf_study.DesignPattern.MVVM.Models;

namespace Wpf_study
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void MVVMBtnClick(object sender, RoutedEventArgs e)
        {
            var personRepository = new PersonRepository();
            var mainView = new DesignPattern.MVVM.Views.MainView()
            {
                DataContext = new DesignPattern.MVVM.ViewModels.MainViewModel(personRepository)
            };

            mainView.Show();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
