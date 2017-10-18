using FilmAPI.Common.DTOs.Film;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Film;

namespace FilmAPI.Mappers
{
    public class FilmMapper : BaseMapper<Film, BaseFilmDto>, IFilmMapper
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
