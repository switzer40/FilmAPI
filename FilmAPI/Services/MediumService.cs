using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;
using System.Collections.Generic;

namespace FilmAPI.Services
{
    public class MediumService : IMediumService
    {
        private readonly IMediumRepository _repository;
        private readonly IMediumMapper _mapper;
        private readonly IKeyService _keyService;
        private readonly IFilmRepository _filmRepository;
        public MediumService(IMediumRepository repository, IMediumMapper mapper, IKeyService keyService, IFilmRepository filmRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
            _filmRepository = filmRepository;
        }
        public MediumViewModel Add(MediumViewModel m)
        {
            var mediumToAdd = _mapper.MapBack(m);
            var savedMedium = _repository.Add(mediumToAdd);
            return _mapper.Map(savedMedium);
        }

        public async Task<MediumViewModel> AddAsync(MediumViewModel m)
        {
            return await Task.Run(() => Add(m));
        }

        public void Delete(MediumViewModel m)
        {
            var mediumToDelete = _mapper.MapBack(m);
            _repository.Delete(mediumToDelete);
        }

        public void Delete(string key)
        {
            _keyService.DeconstructMedumSurrogateKey(key);
            int id = _keyService.MediumFilmId;            
            _repository.Delete(id);
        }

        public async Task DeleteAsync(MediumViewModel m)
        {
            await Task.Run(() => Delete(m));
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<MediumViewModel> GetAll()
        {
            List<Medium> media = _repository.List();
            var models = _mapper.MapList(media);
            foreach (var m in models)
            {
                m.SurrogateKey = _keyService.ConstructMediumSurrogateKey(m);
            }
            return models;
        }

        public async Task<List<MediumViewModel>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public MediumViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructMedumSurrogateKey(key);
            int id = _keyService.MediumFilmId;
            var film = _filmRepository.GetById(id);
            return new MediumViewModel(film, _keyService.MediumMediumType, key);
        }

        public async Task<MediumViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public void Update(MediumViewModel m)
        {
            var mediumToUpdate = _mapper.MapBack(m);
            _repository.Update(mediumToUpdate);
        }

        public async Task UpdateAsync(MediumViewModel m)
        {
            await Task.Run(() => Update(m));
        }
    }
}
