using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.VviewModls
{
    public class FilmPersonViewModel: BaseViewModel
    {
        public FilmPersonViewModel(Film f, Person p, string role)
        {
            FilmTitle = f.Title;
            FilmYear = f.Year;
            PersonLasrName = p.LastName;
            PersonBirthdate = p.BirthdateString;
            Role = role;
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
    }
}
