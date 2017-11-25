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
using FilmAPI.Common.Constants;

namespace FilmAPI.Services
{
    public class PersonService : BaseSevice<Person>, IPersonService
    {
        public PersonService(IPersonRepository repo,
                             IPersonMapper mapper) : base(repo, mapper)
        {
        }

        public override OperationStatus Delete(string key)
        {
            var result = OperationStatus.OK;
            (string lastName, string birthdate) data = _keyService.DeconstructPersonKey(key);
            if (data.lastName == FilmConstants.BADKEY)
            {
                result = OperationStatus.BadRequest;
            }
            var personToDelete = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(data.lastName, data.birthdate);
            if (personToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(personToDelete.Id);
            }            
            return result;
        }

        public override OperationStatus Update(IBaseDto<Person> dto)
        {
            var result = OperationStatus.OK;
            var b = (BasePersonDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var personToUpdate = _mapper.MapBack(b);
            var storedPerson = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(b.LastName, b.Birthdate);
            if (storedPerson == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(personToUpdate);
            }
            return result;
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
