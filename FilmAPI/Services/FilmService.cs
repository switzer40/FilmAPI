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
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _repository;
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        public FilmService(IFilmRepository repository, IMapper mapper, IKeyService keyService)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
        }
        public FilmViewModel Add(FilmViewModel m)
        {
            var filmToAdd = _mapper.Map<Film>(m);
            var savedFilm = _repository.Add(filmToAdd);
            return _mapper.Map<FilmViewModel>(savedFilm);
        }

        public async Task<FilmViewModel> AddAsync(FilmViewModel m)
        {
            var filmToAdd = _mapper.Map<Film>(m);
            var savedFilm = await _repository.AddAsync(filmToAdd);
            return _mapper.Map<FilmViewModel>(savedFilm);
        }

        public async Task DeleteAsync(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            var filmToDelete = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            await _repository.DeleteAsync(filmToDelete);
        }

        public void Delete(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            var filmToDelete = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            _repository.Delete(filmToDelete);
        }

        public List<FilmViewModel> GetAll()
        {
            List<Film> films = _repository.List();
            var models = _mapper.Map<List<FilmViewModel>>(films);
            foreach (var m in models)
            {
                m.SurrogateKey = _keyService.ConstructFilmSurrogateKey(m);
            }
            return models;
        }

        public async Task<List<FilmViewModel>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public FilmViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            var film = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            return _mapper.Map<FilmViewModel>(film);
        }

        public async Task<FilmViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
            
        }

        public void Update(FilmViewModel m)
        {
            var filmToUpdate = _mapper.Map<Film>(m);
            _repository.Update(filmToUpdate);
        }

        public async Task UpdateAsync(FilmViewModel m)
        {
            var filmToUpdate = _mapper.Map<Film>(m);
            await _repository.UpdateAsync(filmToUpdate);
        }
    }
}
