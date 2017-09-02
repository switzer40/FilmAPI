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
    public class FilmPersonService : IFilmPersonService
    {
        private readonly IFilmPersonRepository _repository;
        private readonly IFilmPersonMapper _mapper;
        private readonly IKeyService _keyService;
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonService(IFilmPersonRepository repository,
                                 IFilmPersonMapper mapper,
                                 IKeyService keyService,
                                 IFilmRepository filmRepository,
                                 IPersonRepository personRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
            _filmRepository = filmRepository;
            _personRepository = personRepository;
        }
        public FilmPersonViewModel Add(FilmPersonViewModel m)
        {
            var filmPersonToAdd = _mapper.MapBack(m);
            var savedFilmPerson = _repository.Add(filmPersonToAdd);
            return _mapper.Map(savedFilmPerson);
        }

        public async Task<FilmPersonViewModel> AddAsync(FilmPersonViewModel m)
        {
            return await Task.Run(() => Add(m));
        }

        public void Delete(FilmPersonViewModel m)
        {
            var filmPersonToDelete = _mapper.MapBack(m);
            _repository.Delete(filmPersonToDelete);
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            Delete(modelToDelete);
        }

        public async Task DeleteAsync(FilmPersonViewModel m)
        {
            await Task.Run(() => Delete(m));
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<FilmPersonViewModel> GetAll()
        {
            List<FilmPerson> filmPeople = _repository.List();
            List<FilmPersonViewModel> models = _mapper.MapList(filmPeople);
            foreach (var m in models)
            {
                m.SurrogateKey = _keyService.ConstructFilmPersonSurrorgateKey(m);
            }
            return models;
        }

        public async Task<List<FilmPersonViewModel>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public FilmPersonViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmPersonSurrogateKey(key);
            var f = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            var p = new Person(_keyService.PersonLastName, _keyService.PersonBirthdate);
            return new FilmPersonViewModel(f.Title, f.Year, p.LastName, p.BirthdateString, _keyService.FilmPersonRole, key);
        }

        public async Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(FilmPersonViewModel m)
        {
            var filmPersonToUpdate = _mapper.MapBack(m);
            _repository.Update(filmPersonToUpdate);
        }

        public async Task UpdateAsync(FilmPersonViewModel m)
        {
            await Task.Run(() => Update(m));         
        }
    }
}
