using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class PersonMapper : EntityMapper<Person, PersonViewModel>, IPersonMapper
    {
        private readonly IKeyService _keyService;
        public PersonMapper(IKeyService service)
        {
            _keyService = service;
        }
        public override PersonViewModel Map(Person e)
        {
            string key = _keyService.ConstructPersonSurrogateKey(e.LastName, e.BirthdateString);
            return new PersonViewModel(e, key);
        }

        public override Person MapBack(PersonViewModel m)
        {
            return new Person(m.LastName, m.BirthdateString, m.FirstMidName);
        }
    }
}
