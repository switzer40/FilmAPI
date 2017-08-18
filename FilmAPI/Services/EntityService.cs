using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using FilmAPI.VviewModls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public abstract class EntityService<EntityType, ModelType> : IEntityService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        protected readonly IRepository<EntityType> _repository;
        protected readonly IMapper _mapper;
        protected readonly IKeyService _keyService;
        public EntityService(IRepository<EntityType> repository, IMapper mapper, IKeyService keyService)
        {
            _repository = repository;
            _mapper = mapper;
            _keyService = keyService;
        }

        public ModelType Add(ModelType m)
        {
            var entityToAdd = _mapper.Map<EntityType>(m);
            var addedEntity = _repository.Add(entityToAdd);
            return _mapper.Map<ModelType>(addedEntity);
        }

        public Task<ModelType> AddAsync(ModelType m)
        {
            throw new NotImplementedException();
        }

        public void Delete(ModelType m)
        {
            var entityToDelete = _mapper.Map<EntityType>(m);
            _repository.Delete(entityToDelete);
        }

        public async Task DeleteAsync(ModelType m)
        {
            var entityToDelete = _mapper.Map<EntityType>(m);
            await _repository.DeleteAsync(entityToDelete);
        }

        public List<ModelType> GetAall()
        {
            var rawEntities = _repository.List(); ;
            return _mapper.Map<List<ModelType>>(rawEntities);
        }

        public async Task<List<ModelType>> GetAllAsync()
        {
            var rawEntities = await _repository.ListAsync();
            return _mapper.Map<List<ModelType>>(rawEntities);
        }

        public abstract ModelType GetBySurrogateKey(string key);


        public abstract Task<ModelType> GetBySurrogateKeyAsync(string key);
      

        public void Update(ModelType m)
        {
            var entityToUpdate = _mapper.Map<EntityType>(m);
            _repository.Update(entityToUpdate);
        }

        public async Task UpdateAsync(ModelType m)
        {
            var entityToUpdate = _mapper.Map<EntityType>(m);
            await _repository.UpdateAsync(entityToUpdate);
        }        
    }
}
