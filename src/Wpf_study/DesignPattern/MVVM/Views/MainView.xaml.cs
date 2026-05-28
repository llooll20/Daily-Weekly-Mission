using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf_study.DesignPattern.MVVM.Models;
using Wpf_study.DesignPattern.MVVM.Services.Time;
using Wpf_study.DesignPattern.MVVM.ViewModels;

namespace Wpf_study.DesignPattern.MVVM.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = CreateViewModel();
        }

        private static MainViewModel CreateViewModel()
        {
            var personRepository = new PersonRepository();
            var clock = new SystemClock();
            var missionPeriodService = new MissionPeriodService(clock);

            return new MainViewModel(personRepository, missionPeriodService);
        }
    }
}
