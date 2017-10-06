using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.Film
{
    public class BaseFilmDto
    {
        private BaseFilmDto()
        {

        }
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
