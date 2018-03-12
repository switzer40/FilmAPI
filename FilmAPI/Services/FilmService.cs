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
    public class FilmService : BaseService<Film>, IFilmService
    {
        private readonly IFilmValidator _validator;
        public FilmService(IFilmRepository repo,
                           IFilmMapper mapper,
                           IFilmValidator validator) : base(repo, mapper)
        {
            _validator = validator;
        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var b = (BaseFilmDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return new OperationResult<IKeyedDto>(vstatus);
            }
            var filmToAdd = _mapper.MapBack(b);
            var (status, value) = _repository.Add(filmToAdd);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var (status, value) = ((IFilmRepository)_repository).GetByKey(key);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public OperationResult<IKeyedDto> GetByTitleAndYear(string title, short year)
        {
            var (status, value) = ((IFilmRepository)_repository).GetByTitleAndYear(title, year);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public async Task<OperationResult<IKeyedDto>> GetByTitleAndYearAsync(string title, short year)
        {
            return await Task.Run(() => GetByTitleAndYear(title, year));
        }

        public override OperationResult<IKeyedDto> GetLastEntry()
        {
            var (status, value) = _repository.GetLastEntry();
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseFilmDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return vstatus;
            }
            var filmToUpdate = _mapper.MapBack(b);
            return _repository.Update(filmToUpdate);
        }

        protected override IKeyedDto RecoverKeyedEntity(Film f)
        {
            var key = _keyService.ConstructFilmKey(f.Title, f.Year);
            return new KeyedFilmDto(f.Title, f.Year, f.Length, key);
        }
    }
}
