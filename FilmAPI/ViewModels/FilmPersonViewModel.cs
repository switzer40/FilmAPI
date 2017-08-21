using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class FilmPersonViewModel: BaseViewModel
    {
        public FilmPersonViewModel()
        {
        }
        public FilmPersonViewModel(Film f, Person p, string role, string key)
        {
            FilmTitle = f.Title;
            FilmYear = f.Year;
            PersonLastName = p.LastName;
            PersonBirthdate = p.BirthdateString;
            Role = role;
            SurrogateKey = key;
        }
        [Required]
        public string FilmTitle { get; set; }
        [Required]
        public short FilmYear { get; set; }
        [Required]
        public string PersonLastName { get; set; }
        [Required]
        public string PersonBirthdate { get; set; }        
        [Required]
        public string Role { get; set; }

        
public override string SurrogateKey { get; set; }
    }
}
