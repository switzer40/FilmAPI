using FilmAPI.DTOs.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Film
{
    public interface IFilmMapper : IHomebrewMapper<Core.Entities.Film, BaseFilmDto>
    {
    }
}
