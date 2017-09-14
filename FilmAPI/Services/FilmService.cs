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
    public class FilmService : EntityService<Film, FilmViewModel>, IFilmService
    {
        public FilmService(IFilmRepository repo, IMapper mapper, IKeyService keyService) : base(mapper, keyService)
        {
            _repository = repo;
        }
        public override Film CreateEntity(string key)
        {
            var data = GetData(key);
            if (data.title == FilmConstants.BADKEY)
            {
                return null;
            }
            return new Film(data.title, data.year);
        }

        private (string title, short year) GetData(string key)
        {
            return _keyService.DeconstructFilmSurrogateKey(key);
        }

        public override FilmViewModel EntityToModel(Film e)
        {
            string key = _keyService.ConstructFilmSurrogateKey(e.Title, e.Year);
            return new FilmViewModel(e, key);
        }

        public override Film GetEntity(string key)
        {
            var data = GetData(key);
            if (data.title == FilmConstants.BADKEY)
            {
                return null;
            }
            return ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year);
        }

        public override Film ModelToEntity(FilmViewModel m)
        {
            return _mapper.Map<Film>(m);
        }        
    }
}
