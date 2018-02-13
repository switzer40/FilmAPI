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
    public class FilmService : BaseService<Film>, IFilmService
    {
        private readonly IFilmValidator _validator;
        public FilmService(IFilmRepository repo,
                           IFilmMapper mapper,
                           IFilmValidator validator) :base(repo, mapper)
        {
            _validator = validator;
        }

        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var retVal = OperationStatus.OK;
            var b = (BaseFilmDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;

            if (IsValid)
            {
                var filmToAdd = _mapper.MapBack(b);
                var val = _repository.Add(filmToAdd).value;
                result = RecoverKeyedEntity(val);
            }
            else
            {
                result = default;
                retVal = OperationStatus.BadRequest;
                retVal.ReasonForFailure = "Invalid argument";
            }
            return new OperationResult<IKeyedDto>(retVal, result);
        }

        public override OperationStatus Delete(string key)
        {
            var data = _keyService.DeconstructFilmKey(key);
            var filmToDelete = ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year).value;
            return _repository.Delete(filmToDelete);
        }

        public override OperationResult<List<IKeyedDto>> GetAbsolutelyAll()
        {
            var status = OperationStatus.OK;
            var val = new List<IKeyedDto>();
            var res = _repository.List();
            var flist =res.value;
            status = res.status;
            var list = _mapper.MapList(flist);
            foreach (var f in flist)
            {
                val.Add(RecoverKeyedEntity(f));
            }
            return new OperationResult<List<IKeyedDto>>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var data = _keyService.DeconstructFilmKey(key);
            var res = ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year);
            var status = res.status;
            var val = (IKeyedDto)res.value;
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseFilmDto)dto;
            var filmToUpdate = new Film(b.Title, b.Year, b.Length);
            return _repository.Update(filmToUpdate);
        }
                
        protected override IKeyedDto RecoverKeyedEntity(Film value)
        {
            var b = (BaseFilmDto)_mapper.Map(value);
            var key = _keyService.ConstructFilmKey(b.Title, b.Year);
            return new KeyedFilmDto(b.Title, b.Year, b.Length, key);
        }
    }
}
