using FilmAPI.Interfaces.Medium;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;

namespace FilmAPI.Services.Medium
{
    public class MediumService : IMediumService
    {
        private readonly IMediumRepository _repository;
        private readonly IFilmRepository _filmRepository;
        private readonly IMediumMapper _mapper;
        private readonly IKeyService _keyService;
        public MediumService(IMediumRepository repo, IFilmRepository frepo, IMediumMapper mapper)
        {
            _repository = repo;
            _filmRepository = frepo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public KeyedMediumDto Add(BaseMediumDto m, bool force = false)
        {           
            var mediumToAdd = (force) ? _mapper.MapBackForce(m) :  _mapper.MapBack(m);
            var savedMedium = _repository.Add(mediumToAdd);
            var key = _keyService.ConstructMediumKey(m.Title, m.Year, m.MediumType);
            var result = new KeyedMediumDto(m.Title, m.Year, m.MediumType, m.Location, m.Length, key);                      
            return result;
        }

        public async Task<KeyedMediumDto> AddAsync(BaseMediumDto m, bool force)
        {
            return await Task.Run(() => Add(m, force));
        }

        public void Delete(string key)
        {
            var modelToDelete = GetBySurrogateKey(key);
            var mediumToDelete = _mapper.MapBack(modelToDelete);
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
            var result = new List<KeyedMediumDto>();
            var media = _repository.List();
            var baseList = _mapper.MapList(media);
            foreach (var item in baseList)
            {
                var key = _keyService.ConstructMediumKey(item.Title, item.Year, item.MediumType);
                var keyedItem = new KeyedMediumDto(item.Title, item.Year, item.MediumType,  item.Location, item.Length, key);                                   
                result.Add(keyedItem);
            }
            return result;
        }

        public async Task<List<KeyedMediumDto>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public KeyedMediumDto GetBySurrogateKey(string key)
        {
            var data = _keyService.DeconstructMediumKey(key);
            var f = _filmRepository.GetByTitleAndYear(data.title, data.year);
            var m = _repository.GetByFilmIdAndMediumType(f.Id, data.mediumType);
            var keyedMedium = new KeyedMediumDto(data.title, data.year, data.mediumType, m.Location, f.Length, key);            
            return keyedMedium;
        }

        public void Update(BaseMediumDto m)
        {
            var mediumToUpdate = _mapper.MapBack(m);
            _repository.Update(mediumToUpdate);
        }

        public async Task UpdateAsync(BaseMediumDto m)
        {
            await Task.Run(() => Update(m));
        }

        public async Task<KeyedMediumDto> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }
    }
}
