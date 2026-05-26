using System;

namespace Wpf_study.DesignPattern.MVVM.Models
{
    public class Person
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public int Age { get; set; } = 0;

        public void Update(Person person)
        {
            Name = person.Name;
            Sex = person.Sex;
            Age = person.Age;
        }
    }
}
