using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.FilmPerson
{
    public class BaseFilmPersonDto
    {
        public BaseFilmPersonDto(string title,
                                 short year,
                                 string lastName,
                                 string birthdate,
                                 string role,
                                 short length = 0,
                                 string firstMidName = "")
        {
            Title = title;
            Year = year;
            LastName = lastName;
            Birthdate = birthdate;
            Role = role;
            Length = length;
            FirstMidName = firstMidName;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1850, 2050)]
        public short Year { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Birthdate { get; set; }
        [Required]
        public string Role { get; set; }
        public short Length { get; set; }
        public string FirstMidName { get; set; }
    }
}
