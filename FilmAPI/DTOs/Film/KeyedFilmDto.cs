using FilmAPI.DTOs.Film;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.Film
{
    public class KeyedFilmDto : BaseFilmDto, IFilmDto
    {
              
        

        public KeyedFilmDto(string title, short year, short length = 0) : base(title, year, length)
        {          
        }
        public string SurrogateKey { get; set; }

    }
}
