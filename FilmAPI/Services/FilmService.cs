using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Constants;
using FilmAPI.Common.Validators;
using FluentValidation.Results;
using FluentValidation;

namespace FilmAPI.Services
{
    public class FilmService : BaseSevice<Film>, IFilmService
    {
        private readonly IValidator<BaseFilmDto> _validator;
        public FilmService(IFilmRepository repo,
                           IFilmMapper mapper,
                           IValidator<BaseFilmDto> validator) : base(repo, mapper)
        {
            _validator = validator;
        }
        public KeyedFilmDto Result()
        {
            return (KeyedFilmDto)result;
        }
        public override OperationStatus Add(IBaseDto<Film> dto)
        {
            var retVal = OperationStatus.OK;
            var b = (BaseFilmDto)dto;            
            var results =  _validator.Validate(b);
            IsValid = results.IsValid;
            Failures.AddRange(results.Errors);
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
            (string title, short year) data = _keyService.DeconstructFilmKey(key);
            if (data.title == FilmConstants.BADKEY)
            {
                result = OperationStatus.BadRequest;
            }
            var filmToDelete = ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year);
            if (filmToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(filmToDelete);
            }
            return result;
        }

        public override OperationStatus Update(IBaseDto<Film> dto)
        {
            var result = OperationStatus.OK;
            var b = (BaseFilmDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var filmToUpdate = _mapper.MapBack(b);
            var storedFilm = ((IFilmRepository)_repository).GetByTitleAndYear(b.Title, b.Year);
            if (storedFilm == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(filmToUpdate);
            }
            return result;
        }

        protected override IKeyedDto<Film> ExtractKeyedDto(IBaseDto<Film> dto)
        {
            var b = (BaseFilmDto)dto;
            var key = _keyService.ConstructFilmKey(b.Title, b.Year);
            var result = new KeyedFilmDto(b.Title, b.Year, b.Length, key);
            return (IKeyedDto<Film>)result;
        }

        protected override Film RetrieveStoredEntity(IBaseDto<Film> dto)
        {
            var b = (BaseFilmDto)dto;
            return ((IFilmRepository)_repository).GetByTitleAndYear(b.Title, b.Year);
        }

        object IFilmService.Result()
        {
            return (KeyedFilmDto)result;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
