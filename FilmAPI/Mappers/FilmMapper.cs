using FilmAPI.DTOs.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Film;

namespace FilmAPI.Mappers
{
    public class FilmMapper : BaseMapper<Core.Entities.Film, BaseFilmDto>, IFilmMapper
    {
        public override BaseFilmDto Map(Film e)
        {
            var result = new BaseFilmDto(e.Title, e.Year, e.Length);            
            return result;
        }

        public override Film MapBack(BaseFilmDto m)
        {
            return new Film(m.Title, m.Year, m.Length);
        }
    }
}
