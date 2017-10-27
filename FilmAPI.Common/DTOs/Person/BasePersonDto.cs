using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Common.DTOs.Person
{
    public class BasePersonDto
    {
        public BasePersonDto(string lastName, string birthdate, string firstMidName = "")
        {
            LastName = lastName;
            Birthdate = birthdate;
            FirstMidName = firstMidName;
        }
        public string FirstMidName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Birthdate { get; set; }
    }
}
