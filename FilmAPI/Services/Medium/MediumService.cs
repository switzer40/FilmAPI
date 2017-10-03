using FilmAPI.Interfaces.Medium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.DTOs.Film;
using FilmAPI.DTOs.Medium;
using FilmAPI.Core.Interfaces;
using AutoMapper;
using FilmAPI.Interfaces;

namespace FilmAPI.Services.Medium
{
    public class MediumService : IMediumService
    {
        private readonly IMediumRepository _repository;
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        public MediumService(IMediumRepository repo, IFilmRepository frepo, IMapper mapper)
        {
            _repository = repo;
            _filmRepository = frepo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public KeyedMediumDto Add(BaseMediumDto m)
        {
            var mediumToAdd = _mapper.Map<Core.Entities.Medium>(m);
            var savedMedium = _repository.Add(mediumToAdd);
            var result = _mapper.Map<KeyedMediumDto>(savedMedium);
            result.SurrogateKey = _keyService.ConstructMediumSurrogateKey(result.Title, result.Year, result.MediumType);
            return result;
        }

        public async Task<KeyedMediumDto> AddAsync(BaseMediumDto m)
        {
            return await Task.Run(() => Add(m));
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            var mediumToDelete = _mapper.Map<Core.Entities.Medium>(modelToDelete);
            // If this is theonly medium transporting the given film, then we might
            // as well delete the film as well.
            CascadefSensible(modelToDelete);
            _repository.Delete(mediumToDelete);
        }

        private void CascadefSensible(KeyedMediumDto m)
        {
            var f = _filmRepository.GetByTitleAndYear(m.Title, m.Year);
            var count = _repository.CountMediaByFilmId(f.Id);
            if (count == 1)
            {
                var f1 = _filmRepository.GetByTitleAndYear(m.Title, m.Year);
                _filmRepository.Delete(f1);
            }
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<KeyedMediumDto> GetAll()
        {
            var media = _repository.List();
            var modelList = _mapper.Map<List<KeyedMediumDto>>(media);
            foreach (var item in modelList)
            {
                item.SurrogateKey =
                    _keyService.ConstructMediumSurrogateKey(item.Title, item.Year, item.MediumType);
            }
            return modelList;
        }

        public async Task<List<KeyedMediumDto>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public KeyedMediumDto GetBySurrogateKey(string key)
        {
            var data = _keyService.DeconstructMediumSurrogateKey(key);
            var f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            var m = _repository.GetByFilmIdAndMediumType(f.Id, data.mediumType);
            var keyedMedium = new KeyedMediumDto(data.title, data.year, data.mediumType, m.Location);
            keyedMedium.SurrogateKey = key;
            return keyedMedium;
        }

        public void Update(KeyedMediumDto m)
        {
            var mediumToUpdate = _mapper.Map<Core.Entities.Medium>(m);
            _repository.Update(mediumToUpdate);
        }

        public async Task UpdateAsync(KeyedMediumDto m)
        {
            await Task.Run(() => Update(m));
        }

        public async Task<KeyedMediumDto> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }
        
    }
}
