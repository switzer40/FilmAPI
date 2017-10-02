using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.Film
{
    public partial class BaseFilmDto
    {        
        public BaseFilmDto(string title, short year, short length = 0)
        {
            Title = title;
            Year = year;
            Length = length;
        }

        public string Title { get; set; }
        public short Year { get; set; }
        public short Length { get; set; }
    }
}
