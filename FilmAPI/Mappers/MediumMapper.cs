using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class MediumMapper : BaseMapper<Medium>, IMediumMapper
    {
        private readonly IFilmRepository _filmRepository;
        public MediumMapper(IFilmRepository frepo)
        {
            _filmRepository = frepo;
        }
        public override IBaseDto Map(Medium t)
        {
            var f = _filmRepository.GetById(t.FilmId);
            if (f == null)
            {
                throw new Exception("Unknown film");
            }
            return new BaseMediumDto(f.Title, f.Year, t.MediumType, t.Location);
        }

        public override Medium MapBack(IBaseDto dto)
        {
            var b = (BaseMediumDto)dto;
            var f = _filmRepository.GetByTitleAndYear(b.Title, b.Year);
            if (f == null)
            {
                throw new Exception("Unknown film");
            }
            return new Medium(f.Id, b.MediumType);
        }
    }
}