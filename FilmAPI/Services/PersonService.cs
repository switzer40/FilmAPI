using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.ViewModels;
using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Entities;

namespace FilmAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        public PersonService(IPersonRepository repository, IMapper mapper, IKeyService keyService)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
        }
        public PersonViewModel Add(PersonViewModel m)
        {
            var personToAdd = _mapper.Map<Person>(m);
            var savedPerson = _repository.Add(personToAdd);
            return _mapper.Map<PersonViewModel>(savedPerson);
        }

        public async Task<PersonViewModel> AddAsync(PersonViewModel m)
        {
            var personToAdd = _mapper.Map<Person>(m);
            var savedPerson = await _repository.AddAsync(personToAdd);
            return _mapper.Map<PersonViewModel>(savedPerson);
        }

        public void Delete(PersonViewModel m)
        {
            var personToDelete = _mapper.Map<Person>(m);
            _repository.Delete(personToDelete);
        }

        public void Delete(string key)
        {
            _keyService.DeconstructPesonSurrogateKey(key);
            var personToDelete = new PersonViewModel(_keyService.PersonLastName, _keyService.PersonBirthdate);
            Delete(personToDelete);
        }

        public async Task DeleteAsync(PersonViewModel m)
        {
            var personToDelete = _mapper.Map<Person>(m);
            await _repository.DeleteAsync(personToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            _keyService.DeconstructPesonSurrogateKey(key);
            var personToDelete = new PersonViewModel(_keyService.PersonLastName, _keyService.PersonBirthdate);
            await DeleteAsync(personToDelete);
        }

        public List<PersonViewModel> GetAll()
        {
            List<Person> people = _repository.List();
            return _mapper.Map<List<PersonViewModel>>(people);
        }

        public async Task<List<PersonViewModel>> GetAllAsync()
        {
            List<Person> people = await _repository.ListAsync();
            return _mapper.Map<List<PersonViewModel>>(people);
        }

        public PersonViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructPesonSurrogateKey(key);
            return new PersonViewModel(_keyService.PersonLastName, _keyService.PersonBirthdate);
        }

        public async Task<PersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(PersonViewModel m)
        {
            var personToUpdate = _mapper.Map<Person>(m);
            _repository.Update(personToUpdate);
        }

        public async Task UpdateAsync(PersonViewModel m)
        {
            var personToUpdate = _mapper.Map<Person>(m);
            await _repository.UpdateAsync(personToUpdate);
        }
    }
}
