using FilmAPI.Interfaces.FilmPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.FilmPerson
{
    public class KeyedFilmPersonDto : BaseFilmPersonDto, IFilmPersonDto
    {
       
        public KeyedFilmPersonDto(string title,
                                  short year,
                                  string lastName,
                                  string birthdate,
                                  string role,
                                  short length,
                                  string firstMidName,
                                  string key) : base(title, year, lastName, birthdate, role, length, firstMidName)
        {
            Surrogateey = key;
        }

        public string Surrogateey { get; set; }
    }
}
