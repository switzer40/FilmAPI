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
    public class FilmPersonService : EntityService<FilmPerson, FilmPersonViewModel>, IFilmPersonService
    {
        public FilmPersonService(IRepository<FilmPerson> repository, IMapper mapper, IKeyService keyService) : base(repository, mapper, keyService)
        {
        }

        public override FilmPersonViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmPersonSurrogateKey(key);
            return new FilmPersonViewModel(_keyService.FilmPersonFilmId, _keyService.FilmPersonPersonId, _keyService.FilmPersonRole);
        }

        public override Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
