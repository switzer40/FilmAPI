using FilmAPI.Common.DTOs.Film;

namespace FilmAPI.Interfaces.Film
{
    public interface IFilmService : IEntityService<Core.Entities.Film, BaseFilmDto, KeyedFilmDto>
    {
    }
}
