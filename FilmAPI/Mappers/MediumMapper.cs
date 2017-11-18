using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Mappers
{
    public class MediumMapper : BaseMapper<Medium>, IMediumMapper
    {
        private readonly IFilmRepository _filmRepository;
        public MediumMapper(IFilmRepository frepo)
        {
            _filmRepository = frepo;
        }
        public override IBaseDto<Medium> Map(Medium t)
        {
            var f = _filmRepository.GetById(t.FilmId);
            var result = new BaseMediumDto(f.Title, f.Year, t.MediumType, t.Location);
            return (IBaseDto<Medium>)result;
        }

        public override Medium MapBack(IBaseDto<Medium> dto)
        {
            var b = (BaseMediumDto)dto;
            var f = _filmRepository.GetByTitleAndYear(b.Title, b.Year);
            return new Medium(f.Id, b.MediumType, b.Location);
        }
    }
}
