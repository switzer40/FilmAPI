using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace FilmAPI.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {       
        public PersonViewModel() : base()
        {
        }
        public PersonViewModel(Person p) : base()
        {
            FirstMidName = p.FirstMidName;
            LastName = p.LastName;
            BirthdateString = p.BirthdateString;
            _key = _keyService.ConstructPersonSurrogateKey(LastName, BirthdateString);
        }
        public PersonViewModel(string lastName,
                               string birthdate,
                               string firstMidName = "") : base()
        {
            FirstMidName = firstMidName;
            LastName = lastName;
            BirthdateString = birthdate;
            _key = _keyService.ConstructPersonSurrogateKey(LastName, BirthdateString);
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
        private string _key;
        public override string SurrogateKey { get { return _key; } }
    }
}
