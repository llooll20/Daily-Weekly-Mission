using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Wpf_study.DesignPattern.MVVM.Commands;
using Wpf_study.DesignPattern.MVVM.Models;
using Wpf_study.DesignPattern.MVVM.Services.Time;

namespace Wpf_study.DesignPattern.MVVM.ViewModels
{
    public class MainViewModel
    {
        private readonly IPersonRepository _personrepository;
        private readonly MissionPeriodService missionPeriodService;

        public MainViewModel(IPersonRepository personRepository, MissionPeriodService missionPeriodService)
        {
            _personrepository = personRepository;
            this.missionPeriodService = missionPeriodService;

            Id = this.missionPeriodService.GetDailyKey().ToString("yyyy-MM-dd");
            Name = this.missionPeriodService.GetWeeklyKey().ToString("yyyy-MM-dd");

            People = new ObservableCollection<Person>(_personrepository.GetAll() ?? Enumerable.Empty<Person>());
            SaveCommand = new SaveCommand(this, _personrepository);
        }

        public ICommand SaveCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? CancelCommand { get; set; }

        public ObservableCollection<Person> People { get; }

        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Sex { get; set; } = "";
        public string Age { get; set; } = "";

        public void RefreshPeople()
        {
            People.Clear();

            foreach (Person person in _personrepository.GetAll() ?? Enumerable.Empty<Person>())
            {
                People.Add(person);
            }
        }
    }
}
