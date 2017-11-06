using FilmAPI.Common.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Person
{
    public interface IPersonService :IEntityService<Core.Entities.Person, BasePersonDto, KeyedPersonDto>
    {
    }
}
