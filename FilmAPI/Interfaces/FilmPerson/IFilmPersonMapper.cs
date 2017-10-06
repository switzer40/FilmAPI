using FilmAPI.DTOs.FilmPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonMapper : IHomebrewMapper<Core.Entities.FilmPerson, BaseFilmPersonDto>
    {
    }
}
