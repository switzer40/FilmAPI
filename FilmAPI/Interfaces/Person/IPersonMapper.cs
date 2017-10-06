using FilmAPI.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Person
{
    public interface IPersonMapper : IHomebrewMapper<Core.Entities.Person, BasePersonDto>
    {
    }
}
