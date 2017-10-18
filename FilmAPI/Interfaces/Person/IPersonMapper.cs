using FilmAPI.Common.DTOs.Person;

namespace FilmAPI.Interfaces.Person
{
    public interface IPersonMapper : IHomebrewMapper<Core.Entities.Person, BasePersonDto>
    {
    }
}
