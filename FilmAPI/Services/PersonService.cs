using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Person;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class PersonService : EntityService<Person, BasePersonDto, KeyedPersonDto>, IPersonService
    {
        public PersonService(IPersonRepository repo, IPersonMapper mapper) : base(repo, (BaseMapper<Person, BasePersonDto>)mapper)
        {
        }
        protected override KeyedPersonDto GenerateOutType(Person p)
        {
            var key = _keyService.ConstructPersonKey(p.LastName, p.BirthdateString);
            return new KeyedPersonDto(p.LastName, p.BirthdateString, p.FirstMidName, key);
        }
    }
}
