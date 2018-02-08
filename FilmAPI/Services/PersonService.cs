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
    public class PersonService : BaseSevice<Person>, IPersonService
    {
        private readonly IPersonValidator _validator;
        public PersonService(IPersonRepository repo,
                             IPersonMapper mapper,
                             IPersonValidator validator) : base(repo, mapper)
        {
            _validator = validator;
        }
        public override OperationResult Add(IBaseDto dto)
        {
            var retVal = OperationStatus.OK;
            var b = (BasePersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;

            var entityToAdd = _mapper.MapBack(b);
            var savedEntity = _repository.Add(entityToAdd);
            if (IsValid)
            {
                result.Add(ExtractKeyedDto(b));
                retVal = OperationStatus.OK;
            }
            else
            {
                result = null;
                retVal = OperationStatus.BadRequest;
                retVal.ReasonForFailure = "Invalid input";
            }
            return new OperationResult(retVal, result);
        }

        public override OperationResult ClearAll()
        {
            _repository.ClearAll();
            return new OperationResult(OperationStatus.OK);
        }

        public override OperationResult Delete(string key)
        {
            var result = OperationStatus.OK;
            var personToDelete = ((IPersonRepository)_repository).GetByKey(key);
            if (personToDelete == null)
            {
                result = OperationStatus.NotFound;
                result.ReasonForFailure = "No such person exists";
            }
            else
            {
                _repository.Delete(personToDelete);
            }
            return new OperationResult(result);
        }

        public override OperationResult GetByKey(string key)
        {
            var data = _keyService.DeconstructPersonKey(key);
            var p = GetByLastNameAndBirthdate(data.lastName, data.birthdate);
            result.Add(p);
            return new OperationResult(OperationStatus.OK, result);
        }

        public KeyedPersonDto GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            var p = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(lastName, birthdate);
            var key = _keyService.ConstructPersonKey(lastName, birthdate);
            return new KeyedPersonDto(p.LastName, p.BirthdateString, p.FirstMidName, key);
        }

        public override OperationResult Update(IBaseDto dto)
        {
            var result = OperationStatus.OK;
            var b = (BasePersonDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
                result.ReasonForFailure = "Wrong input type";
            }
            var personToUpdate = _mapper.MapBack(b);
            var storedperson = RetrieveStoredEntity(dto);
            if (storedperson == null)
            {
                result = OperationStatus.NotFound;
                result.ReasonForFailure = "No such person found";
            }
            else
            {
                _repository.Update(personToUpdate);
            }
            return new OperationResult(result);
        }

        protected override IKeyedDto ExtractKeyedDto(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            var key = _keyService.ConstructPersonKey(b.LastName, b.Birthdate);
            return new KeyedPersonDto(b.LastName, b.Birthdate, b.FirstMidName, key);
        }

        protected override Person RetrieveStoredEntity(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            return ((IPersonRepository)_repository).GetByLastNameAndBirthdate(b.LastName, b.Birthdate);
        }
    }
}
