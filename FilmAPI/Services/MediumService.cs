using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Constants;
using FluentValidation;

namespace FilmAPI.Services
{
    public class MediumService : BaseSevice<Medium>, IMediumService
    {
        private readonly IValidator<BaseMediumDto> _validator;
        public MediumService(IMediumRepository repo,
                             IMediumMapper mapper,
                             IValidator<BaseMediumDto> validator): base(repo, mapper)
        {
            _validator = validator;
        }

        public override OperationStatus Add(IBaseDto<Medium> dto)
        {
            var retVal = OperationStatus.OK;
            BaseMediumDto b = (BaseMediumDto)dto;
            var results = _validator.Validate(b);
            IsValid = results.IsValid;
            Failures.AddRange(results.Errors);
            var entityToAdd = _mapper.MapBack(b);
            var savedEntity = _repository.Add(entityToAdd);
            if(IsValid)
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
            (string title, short year, string mediumType) data = _keyService.DeconstructMediumKey(key);
            if (data.title == FilmConstants.BADKEY)
            {
                result = OperationStatus.BadRequest;
            }
            var mediumToDelete = ((IMediumRepository)_repository).GetByTitleYearAndMediumType(data.title, data.year, data.mediumType);
            if (mediumToDelete == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Delete(mediumToDelete);
            }
            return result;
        }

        public object Result()
        {
            throw new NotImplementedException();
        }

        public override OperationStatus Update(IBaseDto<Medium> dto)
        {
            var result = OperationStatus.OK;
            var b = (BaseMediumDto)dto;
            if (b == null)
            {
                result = OperationStatus.BadRequest;
            }
            var mediumToUpdate = _mapper.MapBack(b);
            var storedMedium = ((IMediumRepository)_repository).GetByTitleYearAndMediumType(b.Title, b.Year, b.MediumType);
            if (storedMedium == null)
            {
                result = OperationStatus.NotFound;
            }
            else
            {
                _repository.Update(mediumToUpdate);
            }
            return result;
        }

        protected override IKeyedDto<Medium> ExtractKeyedDto(IBaseDto<Medium> dto)
        {
            var b = (BaseMediumDto)dto;
            var key = _keyService.ConstructMediumKey(b.Title, b.Year, b.MediumType);
            var result = new KeyedMediumDto(b.Title, b.Year, b.MediumType, b.Location, key);
            return (IKeyedDto<Medium>)result;
        }

        protected override Medium RetrieveStoredEntity(IBaseDto<Medium> dto)
        {
            var b = (BaseMediumDto)dto;
            return ((IMediumRepository)_repository).GetByTitleYearAndMediumType(b.Title, b.Year, b.MediumType);
        }
    }
}
