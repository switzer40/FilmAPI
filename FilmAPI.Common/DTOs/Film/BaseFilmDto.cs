using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Common.DTOs.Film
{
    public class BaseFilmDto
    {
        public BaseFilmDto(string title, short year, short length = 0)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1850, 2050)]
        public short Year { get; set; }
        public short Length { get; set; }
    }
}
