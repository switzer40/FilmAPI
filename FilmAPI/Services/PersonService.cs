using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Services
{
    public class PersonService : BaseService<Person>, IPersonService
    {
        private readonly IPersonValidator _validator;
        public PersonService(IPersonRepository repo,
                             IPersonMapper mapper,
                             IPersonValidator validator) : base(repo,mapper)
        {
            _validator = validator;
        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {            
            var b = (BasePersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            var personToAdd = _mapper.MapBack(b);
            var res = _repository.Add(personToAdd);
            var retVal = res.status;
            var val = RecoverKeyedEntity(res.value);

            if (!IsValid)
            {
                val = default;
                retVal = OperationStatus.BadRequest;
                retVal.ReasonForFailure = "Invalid input";
            }
            return new OperationResult<IKeyedDto>(retVal, val);
        }

        public override OperationStatus Delete(string key)
        {
            return _repository.Delete(key);
        }

        public override OperationResult<List<IKeyedDto>> GetAbsolutelyAll()
        {
            var val = new List<IKeyedDto>();
            var res = _repository.List();
            foreach (var p in res.value)
            {
                val.Add(RecoverKeyedEntity(p));
            }
            return new OperationResult<List<IKeyedDto>>(res.status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var (status, value) = ((IPersonRepository)_repository).GetByKey(key);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public KeyedPersonDto GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            var (status, value) = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(lastName, birthdate);
            return (KeyedPersonDto)RecoverKeyedEntity(value);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            var personToUpdate = _mapper.MapBack(b);
            var status = OperationStatus.OK;
            if (!IsValid)
            {
                status = OperationStatus.BadRequest;
                status.ReasonForFailure = "Invalid input";
            }
            else
            {
                status = _repository.Update(personToUpdate);
            }
            return status;
        } 

        protected override IKeyedDto RecoverKeyedEntity(Person p)
        {
            var key = _keyService.ConstructPersonKey(p.LastName, p.BirthdateString);
            return new KeyedPersonDto(p.LastName, p.BirthdateString, p.FirstMidName, key);
        }
    }
}
