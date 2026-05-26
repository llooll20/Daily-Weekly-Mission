using System;
using Wpf_study.DesignPattern.MVVM.Models;
using Wpf_study.DesignPattern.MVVM.ViewModels;

namespace Wpf_study.DesignPattern.MVVM.Commands
{
    public class SaveCommand : CommandBase
    {
        private readonly MainViewModel mainViewModel;
        private readonly IPersonRepository personRepository;

        public SaveCommand(MainViewModel mainViewModel, IPersonRepository personRepository)
        {
            this.mainViewModel = mainViewModel;
            this.personRepository = personRepository;
        }

        private Person GetPerson()
        {
            var person = new Person()
            {
                Name = mainViewModel.Name,
                Sex = mainViewModel.Sex
            };

            int.TryParse(mainViewModel.Id, out int id);
            int.TryParse(mainViewModel.Age, out int age);

            person.Id = id;
            person.Age = age;

            return person;
        }

        private bool IsValidSave(Person person)
        {
            if (person.Id <= 0) return false;
            if (string.IsNullOrEmpty(person.Name)) return false;
            if (string.IsNullOrEmpty(person.Sex)) return false;
            if (person.Age <= 0) return false;

            return true;
        }

        public override bool CanExecute(object? parameter)
        {
            return IsValidSave(GetPerson());
        }

        public override void Execute(object? parameter)
        {
            Person person = GetPerson();

            if (personRepository.SaveOne(person))
            {
                mainViewModel.RefreshPeople();
            }
        }
    }
}
