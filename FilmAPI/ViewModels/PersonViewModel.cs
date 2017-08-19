using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        public PersonViewModel(Person p)
        {
            FirstMidName = p.FirstMidName;
            LastName = p.LastName;
            BirthdateString = p.BirthdateString;
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
    }
}
