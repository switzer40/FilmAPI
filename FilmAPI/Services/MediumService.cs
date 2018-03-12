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
    public class MediumService : BaseService<Medium>, IMediumService
    {
        private readonly IMediumValidator _validator;
        private readonly IFilmRepository _filmRepository;
        public MediumService(IMediumRepository repo,
                             IMediumMapper mapper,
                             IFilmRepository frepo,
                             IMediumValidator validator) : base(repo, mapper)
        {
            _filmRepository = frepo;
            _validator = validator;
        }
        public override OperationResult<IKeyedDto> Add(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return new OperationResult<IKeyedDto>(vstatus);
            }
            var mediumToAdd = _mapper.MapBack(b);
            var (status, value) = _repository.Add(mediumToAdd);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var (status, value) = ((IMediumRepository)_repository).GetByKey(key);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public OperationResult<IKeyedDto> GetByTitleYearAndMediumType(string title, short year, string mediumType)
        {
            var (status, value) = ((IMediumRepository)_repository).GetByTitleYearAndMediumType(title, year, mediumType);
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public async Task<OperationResult<IKeyedDto>> GetByTitleYearAndMediumTypeAsync(string title, short year, string mediumType)
        {
            return await Task.Run(() => GetByTitleYearAndMediumType(title, year, mediumType));
        }

        public override OperationResult<IKeyedDto> GetLastEntry()
        {
            var (status, value) = _repository.GetLastEntry();
            var val = RecoverKeyedEntity(value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            if (!IsValid)
            {
                var vstatus = OperationStatus.BadRequest;
                vstatus.ReasonForFailure = "Invalid input";
                return vstatus;
            }
            var mediumToUpdate = _mapper.MapBack(b);
            return _repository.Update(mediumToUpdate);
        }

        protected override IKeyedDto RecoverKeyedEntity(Medium m)
        {
            var (status, value) = _filmRepository.GetById(m.FilmId);
            var key = _keyService.ConstructMediumKey(value.Title, value.Year, m.MediumType);
            IKeyedDto result = default;
            if (status == OperationStatus.OK)
            {
                result = new KeyedMediumDto(value.Title, value.Year, m.MediumType, m.Location, m.HasGermanSubtitles, key);
            }
            return result;
        }
    }
}
