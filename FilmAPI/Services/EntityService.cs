using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
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
        protected readonly IMapper _mapper;
        protected readonly IKeyService _keyService;
        protected IRepository<EntityType> _repository;
        public EntityService(IMapper mapper, IKeyService keyService)
        {
            _mapper = mapper;
            _keyService = keyService;
        }

        public ModelType Add(string key)
        {
            var entityToAdd = CreateEntity(key);
            var savedEntity = _repository.Add(entityToAdd);
            return EntityToModel(savedEntity);
        }

        public ModelType Add(ModelType m)
        {
            var entityToAdd = ModelToEntity(m);
            var savedEntity = _repository.Add(entityToAdd);
            return EntityToModel(savedEntity);
        }

        public async Task<ModelType> AddAsync(string key)
        {
            if (key == null)
            {
                throw new BadKeyException("null");
            }
            return await Task.Run(() => Add(key));
        }

        public async Task<ModelType> AddAsync(ModelType m)
        {
            return await Task.Run(() => Add(m));
        }

        public abstract ModelType AddForce(string key);


        public abstract ModelType AddForce(ModelType m);
        

        public async Task<ModelType> AddForceAsync(string key)
        {
            return await Task.Run(() => AddForce(key));
        }

        public async Task<ModelType> AddForceAsync(ModelType m)
        {
            return await Task.Run(() => AddForce(m));
        }

        public abstract EntityType CreateEntity(string key);
        

        public void Delete(string key)
        {
            var entityToDelete = GetEntity(key);
            if (entityToDelete != null)
            {
                _repository.Delete(entityToDelete);
            }            
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public abstract ModelType EntityToModel(EntityType e);
        
        public List<ModelType> GetAll()
        {
            List<ModelType> result = new List<ModelType>();
            List<EntityType> entities = _repository.List();
            foreach (var entity in entities)
            {
                result.Add(EntityToModel(entity));
            }
            return result;
        }

        public async Task<List<ModelType>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public ModelType GetBySurrogateKey(string key)
        {            
            var entityToReturn = GetEntity(key);
            return (entityToReturn == null) ? null : EntityToModel(entityToReturn);           
        }

        public async Task<ModelType> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }

        public abstract EntityType GetEntity(string key);

        public abstract EntityType ModelToEntity(ModelType m);
        
         

        public void Update(string key)
        {
            var entityToUpdate = GetEntity(key);
            if (entityToUpdate == null)
            {
                throw new BadKeyException();
            }
            _repository.Update(entityToUpdate);
        }

        public void Update(ModelType m)
        {
            var entityToUpdate = ModelToEntity(m);
            _repository.Update(entityToUpdate);
        }

        public async Task UpdateAsync(string key)
        {
            await Task.Run(() => Update(key));
        }

        public async Task UpdateAsync(ModelType m)
        {
            await Task.Run(() => Update(m));
        }
    }
}
