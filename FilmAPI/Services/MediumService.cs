using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
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
    public class MediumService : BaseSevice<Medium>, IMediumService
    {
        private readonly IMediumValidator _validator;
        public MediumService(IMediumRepository repo,
                             IMediumMapper mapper,
                             IMediumValidator validator) : base(repo, mapper)
        {
            _validator = validator;
        }
        public override OperationResult Add(IBaseDto dto)
        {
            var retVal = OperationStatus.OK;
            var b = (BaseMediumDto)dto;
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
            var data = _keyService.DeconstructMediumKey(key);
            if (data.title == FilmConstants.BADKEY)
            {
                result = OperationStatus.BadRequest;
                result.ReasonForFailure = FilmConstants.KeyState_Invalid;
            }
            var mediumToDelete = ((IMediumRepository)_repository).GetByKey(key);
            if (mediumToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(mediumToDelete);
            }
            return new OperationResult(result);
        }

        public override OperationResult GetByKey(string key)
        {
            var data = _keyService.DeconstructMediumKey(key);
            var dto = new KeyedMediumDto(data.title, data.year, data.mediumType);
            dto.Key = key;
            result.Add(dto);
            return StandardResult(OperationStatus.OK);
        }
        
        public override OperationResult Update(IBaseDto dto)
        {
            var result = OperationStatus.OK;
            var b = (BaseMediumDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var mediumToUpdate = _mapper.MapBack(b);
            var storedMedium = RetrieveStoredEntity(dto);
            if (storedMedium == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(mediumToUpdate);
            }
            return new OperationResult(result);
        }

        protected override IKeyedDto ExtractKeyedDto(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var key = _keyService.ConstructMediumKey(b.Title, b.Year, b.MediumType);
            return new KeyedMediumDto(b.Title, b.Year, b.MediumType, b.Location, b.HasGermanSubtitles, key);
        }

        protected override Medium RetrieveStoredEntity(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var key = _keyService.ConstructMediumKey(b.Title, b.Year, b.MediumType);
            return ((IMediumRepository)_repository).GetByKey(key);
        }
    }
}
