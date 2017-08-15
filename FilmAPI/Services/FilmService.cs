using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Services
{
    public class FilmService : EntityService<Film, FilmViewModel>, IFilmService
    {
        public FilmService(IFilmRepository repository, IMapper mapper, IKeyService keyService) : base(repository, mapper, keyService)
        {
        }

        public override FilmViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            return new FilmViewModel(_keyService.FilmTitle, _keyService.FilmYear);
        }

        public override async Task<FilmViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

    }
}
