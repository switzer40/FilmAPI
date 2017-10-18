using FilmAPI.Common.DTOs.Film;

namespace FilmAPI.Interfaces.Film
{
    public interface IFilmMapper : IHomebrewMapper<Core.Entities.Film, BaseFilmDto>
    {
    }
}
