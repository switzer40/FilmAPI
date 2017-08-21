using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public abstract class EntityService<EntityType, ModelType> : IEntityService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        protected readonly IRepository<EntityType> _repository;
        protected readonly IEntityMapper<EntityType, ModelType> _mapService;
        protected readonly IKeyService _keyService;
        public EntityService(IRepository<EntityType> repository, IEntityMapper<EntityType, ModelType> mapper, IKeyService keyService)
        {
            _repository = repository;
            _mapService = mapper;
            _keyService = keyService;
        }
        public ModelType Add(ModelType m)
        {
            EntityType entityToAdd = _mapService.MapBack(m);
            var savedEntity = _repository.Add(entityToAdd);
            return _mapService.Map(savedEntity);
        }

        public async Task<ModelType> AddAsync(ModelType m)
        {
            EntityType entityToAdd = _mapService.MapBack(m);
            var savedEntity = await  _repository.AddAsync(entityToAdd);
            return _mapService.Map(savedEntity);
        }

        public async Task DeleteAsync(ModelType m)
        {
            var entityToDelete = _mapService.MapBack(m);
            await _repository.DeleteAsync(entityToDelete);

        }

        public List<ModelType> GetAll()
        {
            var entities = _repository.List();
            return _mapService.MapList(entities);
        }

        public async Task<List<ModelType>> GetAllAsync()
        {
            
            var entities = await _repository.ListAsync();
            return _mapService.MapList(entities);
        }

        public abstract ModelType GetBySurrogateKey(string key);


        public abstract Task<ModelType> GetBySurrogateKeyAsync(string key);
        

        public void Update(ModelType m)
        {
            EntityType entityToUpdate = _mapService.MapBack(m);
            _repository.Update(entityToUpdate);
        }

        public async Task UpdateAsync(ModelType m)
        {
            EntityType entityToUpdate = _mapService.MapBack(m);
           await _repository.UpdateAsync(entityToUpdate);
        }

        public void Delete(ModelType m)
        {
            EntityType entityToDelete = _mapService.MapBack(m);
            _repository.Delete(entityToDelete);
        }        
    }
}
