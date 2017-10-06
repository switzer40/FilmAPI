using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.DTOs.Medium;
using FilmAPI.Interfaces.Medium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class MediumMapper : BaseMapper<Medium, BaseMediumDto>, IMediumMapper
    {
        private readonly IFilmRepository _filmRepository;
        public MediumMapper(IFilmRepository repo)
        {
            _filmRepository = repo;
        }
        public override BaseMediumDto Map(Medium e)
        {
            var f = _filmRepository.GetById(e.FilmId);        
            return new BaseMediumDto(f.Title, f.Year, e.MediumType, e.Location);
        }

        public override Medium MapBack(BaseMediumDto m)
        {
            var f = _filmRepository.GetByTitleAndYear(m.Title, m.Year);
            return new Medium(f.Id, m.MediumType, m.Location);
        }

        public Medium MapBackForce(BaseMediumDto b)
        {
            var f = _filmRepository.GetByTitleAndYear(b.Title, b.Year);
            if (f == null)
            {

                f = new Film(b.Title, b.Year);
                f = _filmRepository.Add(f);
            }
            return new Medium(f.Id, b.MediumType);
        }

    }
}
