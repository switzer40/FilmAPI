using FilmAPI.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FilmAPI.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        public PersonViewModel()
        {
        }
        public PersonViewModel(Person p, string key)
        {
            FirstMidName = p.FirstMidName;
            LastName = p.LastName;
            BirthdateString = p.BirthdateString;
            SurrogateKey = key;
        }
        public PersonViewModel(string lastName, string birthdate, string firstMidName = "")
        {
            FirstMidName = firstMidName;
            LastName = lastName;
            BirthdateString = birthdate;
        }
        public string FirstMidName { get; set; }
        [Required]
        public string LastName { get; set; }
       
        public string BirthdateString { get; set; }
        public DateTime Birthdate
        {
            get
            {
                return DateTime.Parse(BirthdateString);
            }
        }
        public string FullName
        {
            get
            {
                return $"{FirstMidName} {LastName}";
            }
        }

        public override string SurrogateKey { get; set; }
    }
}
