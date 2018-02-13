using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;
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
    public class FilmPersonService : BaseService<FilmPerson>, IFilmPersonService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IFilmPersonValidator _validator;
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmRepository frepo,
                                 IPersonRepository prepo,
                                 IFilmPersonMapper mapper,
                                 IFilmPersonValidator validator) : base(repo, mapper)
        {
            _filmRepository = frepo;
            _personRepository = prepo;
            _validator = validator;

        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            var filmPersonToAdd = _mapper.MapBack(b);
            var res = _repository.Add(filmPersonToAdd);
            var status = res.status;
            IKeyedDto val = default;
            if (!IsValid)
            {
                status = OperationStatus.BadRequest;
                status.ReasonForFailure = "Invalid input";
            }
            else
            {
                val = RecoverKeyedEntity(res.value);
            }
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Delete(string key)
        {
            return _repository.Delete(key);
        }

        public override OperationResult<List<IKeyedDto>> GetAbsolutelyAll()
        {
            var val = new List<IKeyedDto>();
            var res = _repository.List();
            var status = res.status;
            foreach (var fp in res.value)
            {
                val.Add(RecoverKeyedEntity(fp));
            }
            return new OperationResult<List<IKeyedDto>>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var res = ((IFilmPersonRepository)_repository).GetByKey(key);
            var status = res.status;
            var val = RecoverKeyedEntity(res.value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            var filmPersonToUpdate = _mapper.MapBack(b);
            var status = OperationStatus.OK;
            if (!IsValid)
            {
                status = OperationStatus.BadRequest;
                status.ReasonForFailure = "Invalid input";
            }
            else
            {
                status = _repository.Update(filmPersonToUpdate);
            }
            return status;
        }

        protected override IKeyedDto RecoverKeyedEntity(FilmPerson fp)
        {            
            Film f = _filmRepository.GetById(fp.FilmId).value;
            Person p = _personRepository.GetById(fp.PersonId).value;
            var key = _keyService.ConstructFilmPersonKey(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role);
            return new KeyedFilmPersonDto(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role, key);            
        }
    }
}
