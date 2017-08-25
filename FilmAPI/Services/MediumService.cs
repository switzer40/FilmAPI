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
        private readonly IMapper _mapper;
        private readonly IKeyService _keyService;
        private readonly IFilmRepository _filmRepository;
        public MediumService(IMediumRepository repository, IMapper mapper, IKeyService keyService, IFilmRepository filmRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
            _filmRepository = filmRepository;
        }
        public MediumViewModel Add(MediumViewModel m)
        {
            var mediumToAdd = _mapper.Map<Medium>(m);
            var savedMedium = _repository.Add(mediumToAdd);
            return _mapper.Map<MediumViewModel>(savedMedium);
        }

        public async Task<MediumViewModel> AddAsync(MediumViewModel m)
        {
            var mediumToAdd = _mapper.Map<Medium>(m);
            var savedMedium = await _repository.AddAsync(mediumToAdd);
            return _mapper.Map<MediumViewModel>(savedMedium);
        }

        public void Delete(MediumViewModel m)
        {
            var mediumToDelete = _mapper.Map<Medium>(m);
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
            var mediumToDelete = _mapper.Map<Medium>(m);
            await _repository.DeleteAsync(mediumToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            _keyService.DeconstructMedumSurrogateKey(key);
            int id = _keyService.MediumFilmId;
            await  _repository.DeleteAsync(id);
        }

        public List<MediumViewModel> GetAll()
        {
            List<Medium> media = _repository.List();
            return _mapper.Map<List<MediumViewModel>>(media);
        }

        public async Task<List<MediumViewModel>> GetAllAsync()
        {
            List<Medium> media = await  _repository.ListAsync();
            return _mapper.Map<List<MediumViewModel>>(media);
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
            var mediumToUpdate = _mapper.Map<Medium>(m);
            _repository.Update(mediumToUpdate);
        }

        public async Task UpdateAsync(MediumViewModel m)
        {
            var mediumToUpdate = _mapper.Map<Medium>(m);
            await _repository.UpdateAsync(mediumToUpdate);
        }
    }
}
