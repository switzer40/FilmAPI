using FilmAPI.Interfaces.Person;
using FilmAPI.Core.Entities;
using FilmAPI.Common.DTOs.Person;

namespace FilmAPI.Mappers
{
    public class PersonMapper : BaseMapper<Core.Entities.Person, BasePersonDto>, IPersonMapper
    {
        public override BasePersonDto Map(Person e)
        {
            var result = new BasePersonDto(e.LastName, e.BirthdateString, e.FirstMidName);            
            return result;
        }

        public override Person MapBack(BasePersonDto m)
        {
            return new Person(m.LastName, m.Birthdate, m.FirstMidName);
        }
    }
}
