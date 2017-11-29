using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BaseFilmPersopnDto: IBaseDto<FilmPerson>
    {
        public BaseFilmPersopnDto(string title,
                                  short year,
                                  string lastName,
                                  string birthdate,
                                  string role)
        {
            Title = title;
            Year = year;
            LastName = lastName;
            Birthdate = birthdate;
            Role = role;
        }
        
        public string Title { get; set; }
        
        public short Year { get; set; }
        
        public string LastName { get; set; }
        
        public string Birthdate { get; set; }
        
        public string Role { get; set; }
    }
}
