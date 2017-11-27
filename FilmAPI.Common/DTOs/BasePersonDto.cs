using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BasePersonDto : IBaseDto<Person>
    {
        public BasePersonDto(string lastName, string birthdate, string firstMidName = "")
        {
            LastName = lastName;
            Birthdate = birthdate;
            FirstMidName = firstMidName;
        }
        
        public string LastName { get; set; }
        
        public string Birthdate { get; set; }
        public string FirstMidName { get; set; }
    }
}
