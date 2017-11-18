using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Services
{
    public class PersonService : BaseSevice<Person>, IPersonService
    {
        public PersonService(IPersonRepository repo,
                             IPersonMapper mapper) : base(repo, mapper)
        {
        }

        protected override IKeyedDto<Person> ExtractKeyedDto(IBaseDto<Person> dto)
        {
            var b = (BasePersonDto)dto;
            var key = _keyService.ConstructPersonKey(b.LastName, b.Birthdate);
            var result = new KeyedPersonDto(b.LastName, b.Birthdate, b.FirstMidName, key);
            return (IKeyedDto<Person>)result;

        }

        protected override Person RetrieveStoredEntity(IBaseDto<Person> dto)
        {
            var b = (BasePersonDto)dto;
            return ((IPersonRepository)_repository).GetByLastNameAndBirthdate(b.LastName, b.Birthdate);
        }
    }
}
