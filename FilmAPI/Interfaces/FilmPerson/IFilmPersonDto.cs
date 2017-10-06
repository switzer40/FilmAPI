using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonDto
    {
        string Title { get; set; }
        short Year { get; set; }
        string LastName { get; set; }
        string Birthdate { get; set; }
        string Role { get; set; }
    }
}
