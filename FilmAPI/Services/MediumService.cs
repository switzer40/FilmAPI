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

namespace FilmAPI.Services
{
    public class MediumService : BaseSevice<Medium>, IMediumService
    {        
        public MediumService(IMediumRepository repo,
                             IMediumMapper mapper): base(repo, mapper)
        {            
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
