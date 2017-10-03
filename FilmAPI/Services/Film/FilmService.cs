using FilmAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.DTOs;
using FilmAPI.Core.Interfaces;
using AutoMapper;
using FilmAPI.DTOs.Film;

namespace FilmAPI.Services.Film
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _repository;
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        public FilmService(IFilmRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public KeyedFilmDto Add(BaseFilmDto m)
        {
            Core.Entities.Film filmToAdd = _mapper.Map<Core.Entities.Film>(m);
            var savedFilm = _repository.Add(filmToAdd);
            var result =_mapper.Map<KeyedFilmDto>(savedFilm);
            result.SurrogateKey = _keyService.ConstructFilmSurrogateKey(result.Title, result.Year);
            return result;
        }

        public async Task<KeyedFilmDto> AddAsync(BaseFilmDto m)
        {
            return await Task.Run(() => Add(m));
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            var filmToDelete = _mapper.Map<Core.Entities.Film>(modelToDelete);
            _repository.Delete(filmToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<KeyedFilmDto> GetAll()
        {
            var films = _repository.List();
            var modelList = _mapper.Map<List<KeyedFilmDto>>(films);
            foreach (var item in modelList)
            {
                item.SurrogateKey =
                    _keyService.ConstructFilmSurrogateKey(item.Title, item.Year);
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
            var keyedFilm = new KeyedFilmDto(data.title, data.year, f.Length);
            keyedFilm.SurrogateKey = key;
            return keyedFilm;
        }

        public async Task<KeyedFilmDto> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(KeyedFilmDto m)
        {
            var filmToUpdate = _mapper.Map<Core.Entities.Film>(m);
            _repository.Update(filmToUpdate);
        }

        public async Task UpdateAsync(KeyedFilmDto m)
        {
            await Task.Run(() => Update(m));
        }
    }
}
