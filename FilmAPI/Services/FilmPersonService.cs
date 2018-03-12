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
    public class FilmPersonService : BaseService<FilmPerson>, IFilmPersonService
    {
        private readonly IFilmPersonValidator _validator;
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonService(IFilmPersonRepository repo,
                                 IFilmPersonMapper mapper,
                                 IFilmPersonValidator validator,
                                 IFilmRepository frepo,
                                 IPersonRepository prepo) : base(repo, mapper)
        {
            _validator = validator;
            _filmRepository = frepo;
            _personRepository = prepo;
        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return new OperationResult<IKeyedDto>(vstatus);
            }
            var filmPersonToAdd = _mapper.MapBack(b);
            var (status, value) = _repository.Add(filmPersonToAdd);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var (status, value) = ((IFilmPersonRepository)_repository).GetByKey(key);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public OperationResult<IKeyedDto> GetByTitleYearLastNameBirthdateAndRole(string title, short year, string lastName, string birthdate, string role)
        {
            var (status, value) = ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirthdateAndRole(title, year, lastName, birthdate, role);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public async Task<OperationResult<IKeyedDto>> GetByTitleYearLastNameBirthdateAndRoleAsync(string title, short year, string lastName, string birthdate, string role)
        {
            return await Task.Run(() => GetByTitleYearLastNameBirthdateAndRole(title, year, lastName, birthdate, role));
        }

        public override OperationResult<IKeyedDto> GetLastEntry()
        {
            var (status, value) = _repository.GetLastEntry();
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseFilmPersonDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return vstatus;
            }
            var filmPersonToUpdate = _mapper.MapBack(b);
            return _repository.Update(filmPersonToUpdate);            
        }

        protected override IKeyedDto RecoverKeyedEntity(FilmPerson fp)
        {
            KeyedFilmPersonDto result = default;
            Film f = default;
            Person p = default;
            var (fstatus, fvalue) = _filmRepository.GetById(fp.FilmId);
            if (fstatus == OperationStatus.OK)
            {
                f = fvalue;
            }            
            var (pstatus, pvalue) = _personRepository.GetById(fp.PersonId);
            if (pstatus == OperationStatus.OK)
            {
                p = pvalue;
            }
            if (f != null && p != null)
            {
                var key = _keyService.ConstructFilmPersonKey(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role);
                result = new KeyedFilmPersonDto(f.Title, f.Year, p.LastName, p.BirthdateString, fp.Role, key);
            }
            return result;
        }
    }
}
