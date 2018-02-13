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
    public class MediumService : BaseService<Medium>, IMediumService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMediumValidator _validator;
        public MediumService(IMediumRepository repo,
                             IFilmRepository frepo,
                             IMediumMapper mapper,
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
            var mediumToAdd = _mapper.MapBack(b);
            var res = _repository.Add(mediumToAdd);
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
            foreach (var m in res.value)
            {
                val.Add(RecoverKeyedEntity(m));
            }
            return new OperationResult<List<IKeyedDto>>(status, val);
        }

        public override OperationResult<IKeyedDto> GetByKey(string key)
        {
            var res = ((IMediumRepository)_repository).GetByKey(key);
            var status = res.status;
            var val = RecoverKeyedEntity(res.value);
            return new OperationResult<IKeyedDto>(status, val);
        }

        public override OperationStatus Update(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            var mediumToUpdate = _mapper.MapBack(b);
            var status = OperationStatus.OK;
            if (!IsValid)
            {
                status = OperationStatus.BadRequest;
                status.ReasonForFailure = "Invalid input";
            }
            else
            {
                status = _repository.Update(mediumToUpdate);
            }
            return status;
        }

        protected override IKeyedDto RecoverKeyedEntity(Medium m)
        {
            var f = _filmRepository.GetById(m.FilmId).value;
            var key = _keyService.ConstructMediumKey(f.Title, f.Year, m.MediumType);
            return new KeyedMediumDto(f.Title, f.Year, m.MediumType, m.Location, m.HasGermanSubtitles, key);
        }
    }
}
