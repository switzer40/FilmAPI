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
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class PersonService : BaseService<Person>, IPersonService
    {
        private readonly IPersonValidator _validtor;
        public PersonService(IPersonRepository repo,
                             IPersonMapper mapper,
                             IPersonValidator validator) : base(repo, mapper)
        {
            _validtor = validator;
        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            var results = _validtor.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return new OperationResult<IKeyedDto>(vstatus);
            }
            var personToAdd = _mapper.MapBack(b);
            var (status, value) = _repository.Add(personToAdd);
            if (status != OperationStatus.OK)
            {
                return new OperationResult<IKeyedDto>(status);
            }
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var (status, value) = ((IPersonRepository)_repository).GetByKey(key);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public OperationResult<IKeyedDto> GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            var (status, value) = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(lastName, birthdate);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public async Task<OperationResult<IKeyedDto>> GetByLastNameAndBirthdateAsync(string lastName, string birthdate)
        {
            return await Task.Run(() => GetByLastNameAndBirthdate(lastName, birthdate));
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            var results = _validtor.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return vstatus;
            }
            var personToUpdate = _mapper.MapBack(b);
            return _repository.Update(personToUpdate);
        }

        protected override IKeyedDto RecoverKeyedEntity(Person p)
        {
            var key = _keyService.ConstructPersonKey(p.LastName, p.BirthdateString);
            return new KeyedPersonDto(p.LastName, p.BirthdateString, p.FirstMidName, key);
        }
    }
}
