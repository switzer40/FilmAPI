using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Repositories;
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
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonService(FilmPersonRepository repo,
                                 IMapper mapper,
                                 IKeyService keyService,
                                 IFilmRepository frepo,
                                 IPersonRepository prepo) : base(mapper, keyService)
        {
            _repository = repo;
            _filmRepository = frepo;
            _personRepository = prepo;
        }

        public override FilmPerson CreateEntity(string key)
        {
            var data = GetData(key);
            Film f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            Person p = _personRepository.GetByLastNameAndBirthdate(data.lastName, data.birthdate);
            return new FilmPerson(f.Id, p.Id, data.role);
        }

        private (string title, short year, string lastName, string birthdate, string role) GetData(string key)
        {
            return _keyService.DeconstructFilmPersonSurrogateKey(key);
        }

        public override FilmPersonViewModel EntityToModel(FilmPerson e)
        {
            Film f = _filmRepository.GetById(e.FilmId);
            Person p = _personRepository.GetById(e.PersonId);
            string key = _keyService.ConstructFilmPersonSurrogateKey(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role);
            return new FilmPersonViewModel(f.Title, f.Year, p.LastName, p.BirthdateString, e.Role, key);
        }
        

        public override FilmPerson ModelToEntity(FilmPersonViewModel m)
        {
            return GetEntity((m.SurrogateKey));
        }

        public override FilmPerson GetEntity(string key)
        {
            var data = GetData(key);
            Film f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            Person p = _personRepository.GetByLastNameAndBirthdate(data.lastName, data.birthdate);
            return ((IFilmPersonRepository)_repository).GetByFilmIdPersonIdAndRole(f.Id, p.Id, data.role);                                
        }
    }
}
