using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class MediumService : EntityService <Medium, MediumViewModel>, IMediumService
    {
        private readonly IFilmRepository _filmRepository;
        public MediumService(IMediumRepository repo,
                             IMapper mapper,
                             IKeyService keyService,
                             IFilmRepository frepo) : base(mapper, keyService)
        {
            _repository = repo;
            _filmRepository = frepo;
        }

        public override Medium CreateEntity(string key)
        {
            var data = GetData(key);
            if (data.title == FilmConstants.BADKEY)
            {
                return null;
            }
            Film f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            return new Medium(f.Id, data.mediumType);
        }

        private (string title, short year, string mediumType) GetData(string key)
        {
            return _keyService.DeconstructMediumSurrogateKey(key);
        }

        public override MediumViewModel EntityToModel(Medium e)
        {
            Film f = _filmRepository.GetById(e.FilmId);
            string key = _keyService.ConstructMediumSurrogateKey(f.Title, f.Year, e.MediumType);
           var model = new MediumViewModel(f, e.MediumType, key);
            model.Location = e.Location;
            return model;
        }

        public override Medium GetEntity(string key)
        {
            var data = GetData(key);
            if (data.title == FilmConstants.BADKEY)
            {
                return null;
            }
            Film f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            return ((IMediumRepository)_repository).GetByFilmIdAndMediumType(f.Id, data.mediumType);
        }

        public override Medium ModelToEntity(MediumViewModel m)
        {
            var medium = GetEntity(m.SurrogateKey);
            medium.Location = m.Location;
            return GetEntity(m.SurrogateKey);
        }
    }
}
