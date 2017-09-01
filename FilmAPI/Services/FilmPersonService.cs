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
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        private readonly IFilmRepository _filmRepository;
        private readonly IPersonRepository _personRepository;
        public FilmPersonService(IFilmPersonRepository repository,
                                 IMapper mapper,
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
            var filmPersonToAdd = _mapper.Map<FilmPerson>(m);
            var savedFilmPerson = _repository.Add(filmPersonToAdd);
            return _mapper.Map<FilmPersonViewModel>(savedFilmPerson);
        }

        public async Task<FilmPersonViewModel> AddAsync(FilmPersonViewModel m)
        {
            var filmPersonToAdd = _mapper.Map<FilmPerson>(m);
            var savedFilmPerson = await _repository.AddAsync(filmPersonToAdd);
            return _mapper.Map<FilmPersonViewModel>(savedFilmPerson);
        }

        public void Delete(FilmPersonViewModel m)
        {
            var filmPersonToDelete = _mapper.Map<FilmPerson>(m);
            _repository.Delete(filmPersonToDelete);
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            Delete(modelToDelete);
        }

        public Task DeleteAsync(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public List<FilmPersonViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<FilmPersonViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
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
            var filmPersonToUpdate = _mapper.Map<FilmPerson>(m);
            _repository.Update(filmPersonToUpdate);
        }

        public async Task UpdateAsync(FilmPersonViewModel m)
        {
            var filmPersonToUpdate = _mapper.Map<FilmPerson>(m);
             await  _repository.UpdateAsync(filmPersonToUpdate);           
        }
    }
}
