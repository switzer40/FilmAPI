using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        public PersonViewModel()
        {
        }
        private readonly IKeyService _keyService;
        public PersonViewModel(IKeyService keyService, string lastName, string birthdate, string firstMidName = "")
        {
            _keyService = keyService;
            LastName = lastName;
            BirthdateString = birthdate;
            FirstMidName = firstMidName;
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
        public override string SurrogateKey => _keyService.ConstructPersonSurrogateKey(this);
    };
}
