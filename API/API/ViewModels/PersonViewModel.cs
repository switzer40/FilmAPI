using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        public PersonViewModel(IKeyService service, string lastName, string birthdate, string firstMidName = ""): base(service)
        {
            LastName = lastName;
            BirthdateString = birthdate;
            FirstMidName = firstMidName;
        }
        public override string SurrogateKey()
        {
            return _keyService.ConstructPersonSurrogateKey(LastName, BirthdateString);
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
