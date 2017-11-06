using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class MediumService : EntityService<Medium, BaseMediumDto, KeyedMediumDto>
    {
        private readonly IFilmRepository _filmRepository;
        public MediumService(IMediumRepository repo, BaseMapper<Medium, BaseMediumDto> mapper, IFilmRepository frepo) : base(repo, mapper)
        {
            _filmRepository = frepo;
        }

        protected override KeyedMediumDto GenerateOutType(Medium m)
        {
            var f = _filmRepository.GetById(m.FilmId);
            var key = _keyService.ConstructMediumKey(f.Title, f.Year, m.MediumType);
            return new KeyedMediumDto(f.Title, f.Year, m.MediumType, key);
        }
    }
}
