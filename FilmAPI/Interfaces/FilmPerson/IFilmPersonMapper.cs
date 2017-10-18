using FilmAPI.Common.DTOs.FilmPerson;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonMapper : IHomebrewMapper<Core.Entities.FilmPerson, BaseFilmPersonDto>
    {
        Core.Entities.FilmPerson MapBackForce(BaseFilmPersonDto b);
    }
}
