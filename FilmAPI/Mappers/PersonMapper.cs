using FilmAPI.DTOs.Person;
using FilmAPI.Interfaces.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Core.Entities;

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
            return new Person(m.FirstMidName, m.LastName, m.Birthdate);
        }
    }
}
