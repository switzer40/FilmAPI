using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
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
       [Required]
        public string BirthdateString { get; set; }
        public DateTime Birthdate
        {
            get
            {
                // This looks strange; we don´t want to throw an
                // exception, if the user enters an invalid date string.
                // Some other mechanism will have to prevent that from happening.
                DateTime result = DateTime.Parse(FilmConstants.ImprobableDateString);
                DateTime dummy = DateTime.Now;
                if (DateTime.TryParse(BirthdateString, out dummy))
                {
                    result = dummy;
                }                
                return result;
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
