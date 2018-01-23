using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{

    public class FilmPersonService : BaseSevice<FilmPerson>, IFilmPersonService
    {
        private readonly IFilmPersonValidator _validator;
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmPersonMapper mapper,
                                 IFilmPersonValidator validator) : base(repo, mapper)
        {
            _validator = validator;
        }
        public override OperationStatus Add(IBaseDto dto)
        {
            var retVal = OperationStatus.OK;
            var b = (BaseFilmPersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;

            var entityToAdd = _mapper.MapBack(b);
            var savedEntity = _repository.Add(entityToAdd);
            if (IsValid)
            {
                result = ExtractKeyedDto(b);
                retVal = OperationStatus.OK;
            }
            else
            {
                result = null;
                retVal = OperationStatus.BadRequest;
            }
            return retVal;
        }

        public override OperationStatus Delete(string key)
        {
            var result = OperationStatus.OK;
            var filmPersonToDelete = ((IFilmPersonRepository)_repository).GetByKey(key);
            if (filmPersonToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(filmPersonToDelete);
            }
            return result;
        }

        public override OperationStatus GetByKey(string key)
        {
            var data = _keyService.DeconstructFilmPersonKey(key);
            _getResults[key] = new KeyedFilmPersonDto(data.title, data.year, data.lastName, data.birthdate, data.role);
            _getResults[key].Key = key;
            return OperationStatus.OK;
        }

        public object Result()
        {
            return (KeyedFilmPersonDto)result;
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var result = OperationStatus.OK;
            var b = (BaseFilmPersonDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var filmPersonToUpdate = _mapper.MapBack(b);
            var storedFilmPerson = RetrieveStoredEntity(dto);
            if (storedFilmPerson == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(filmPersonToUpdate);
            }
            return result;
        }

        protected override IKeyedDto ExtractKeyedDto(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var key = _keyService.ConstructFilmPersonKey(b.Title, b.Year, b.LastName, b.Birthdate, b.Role);
            var result = new KeyedFilmPersonDto(b.Title, b.Year, b.LastName, b.Birthdate, b.Role, key);
            return (IKeyedDto)result;
        }

        protected override FilmPerson RetrieveStoredEntity(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            return ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirthdateAndRole(b.Title, b.Year, b.LastName, b.Birthdate, b.Role);
        }
    }
}
