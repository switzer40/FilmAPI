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
    public class FilmPersonService : EntityService<FilmPerson, FilmPersonViewModel>, IFilmPersonService
    {
        public FilmPersonService(IFilmPersonRepository repository, IFilmPersonMapper mapper, KeyService keyService) : base(repository, mapper, keyService)
        {

        }
        public override FilmPersonViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmPersonSurrogateKey(key);
            Film f = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            Person p = new Person(_keyService.PersonLastName, _keyService.PersonBirthdate);
            return new FilmPersonViewModel(f, p, _keyService.FilmPersonRole, key);
        }

        public override async Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run<FilmPersonViewModel>(() => GetBySurrogateKey(key));
        }
    }
}
