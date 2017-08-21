using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class MediumMapper : EntityMapper<Medium, MediumViewModel>, IMediumMapper
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IKeyService _keyService;
        public MediumMapper(IFilmRepository repo, IKeyService service)
        {
            _filmRepository = repo;
            _keyService = service;
        }
        public override  MediumViewModel Map(Medium e)
        {
            Film f = _filmRepository.GetById(e.FilmId);
            return new MediumViewModel(f, e.MediumType, _keyService.ConstructMediumSurrogateKey(f.Title, f.Year, e.MediumType));
        }

        

        public override  Medium MapBack(MediumViewModel m)
        {
            Film f = _filmRepository.GetByTitleAndYear(m.FilmTitle, m.FilmYear);
            return new Medium(f.Id, m.MediumType);
        }
        

    }
}
