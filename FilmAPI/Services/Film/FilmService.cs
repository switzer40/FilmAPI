using FilmAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.DTOs;
using FilmAPI.Core.Interfaces;
using AutoMapper;
using FilmAPI.DTOs.Film;
using FilmAPI.Interfaces.Film;

namespace FilmAPI.Services.Film
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _repository;
        private readonly IFilmMapper _mapper;
        private readonly IKeyService _keyService;
        public FilmService(IFilmRepository repo, IFilmMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public KeyedFilmDto Add(BaseFilmDto m)
        {
            var filmToAdd = _mapper.MapBack(m);
            var savedFilm = _repository.Add(filmToAdd);
            var key = _keyService.ConstructFilmSurrogateKey(m.Title, m.Year);
            var result = new KeyedFilmDto(m.Title, m.Year, key, m.Length);            
            return result;
        }

        public async Task<KeyedFilmDto> AddAsync(BaseFilmDto m)
        {
            return await Task.Run(() => Add(m));
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            var filmToDelete = _mapper.MapBack(modelToDelete);
            _repository.Delete(filmToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<KeyedFilmDto> GetAll()
        {
            var films = _repository.List();
            var baseList = _mapper.MapList(films);
            var modelList = new List<KeyedFilmDto>();
            foreach (var item in baseList)
            {
                var key = _keyService.ConstructFilmSurrogateKey(item.Title, item.Year);
                var keyedItem = new KeyedFilmDto(item.Title, item.Year, key);
                keyedItem.SurrogateKey =
                    _keyService.ConstructFilmSurrogateKey(item.Title, item.Year);
                modelList.Add(keyedItem);
            }
            return modelList;
        }

        public async Task<List<KeyedFilmDto>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public KeyedFilmDto GetBySurrogateKey(string key)
        {
            var data =_keyService.DeconstructFilmSurrogateKey(key);
            var f = _repository.GetByTitleAndYear(data.title, data.year);
            var keyedFilm = new KeyedFilmDto(data.title, data.year,key, f.Length);            
            return keyedFilm;
        }

        public async Task<KeyedFilmDto> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(BaseFilmDto m)
        {
            var filmToUpdate = _mapper.MapBack(m);
            _repository.Update(filmToUpdate);
        }

        public async Task UpdateAsync(BaseFilmDto m)
        {
            await Task.Run(() => Update(m));
        }
    }
}
