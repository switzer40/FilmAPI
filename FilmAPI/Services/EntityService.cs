using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class EntityService<EntityType, ModelType> : IEntityService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        private readonly IRepository<EntityType> _repository;
        private readonly IMapper _mapper;
        public EntityService(IRepository<EntityType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public ModelType GetBySurrogateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<ModelType> GetBySurrogateKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

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
