using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RegisterTaskWPF.Models
{
    public class Person
    {
        private string name;
        private string surname;
        private int age;
        private string city;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Surname { get => surname; set { surname = value; OnPropertyChanged(); } }
        public int Age { get => age; set { age = value; OnPropertyChanged(); } }
        public string City { get => city; set { city = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Person() { }

        public Person(string name, string surname, int age, string city)
        {
            Name = name;
            Surname = surname;
            Age = age;
            City = city;
        }
    }
}