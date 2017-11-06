using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public abstract class EntityService<EntityType, InType, OutType> : IEntityService<EntityType, InType, OutType>
        where EntityType : BaseEntity
        where InType : IBaseDto
        where OutType : IKeyedDto
    {
        private readonly IRepository<EntityType> _repository;
        private readonly BaseMapper<EntityType, InType> _mapper;
        protected KeyService _keyService;
        public EntityService(IRepository<EntityType> repo, BaseMapper<EntityType, InType> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public OutType Add(InType t)
        {
            var entityToAdd = _mapper.MapBack(t);
            var addedEntity = _repository.Add(entityToAdd);
            return GenerateOutType(addedEntity);
        }

        protected abstract OutType GenerateOutType(EntityType addedEntity);
        

        public List<OutType> GetAll()
        {
            List<OutType> result = new List<OutType>();
            var entities = _repository.List();
            foreach (var e in entities)
            {
                result.Add(GenerateOutType(e));
            }
            return result;
        }

        public OutType GetByKey(string key)
        {
            var entity = _repository.GetByKey(key);
            return GenerateOutType(entity);
        }

        public void Remove(string key)
        {
            var entityToRemove = _repository.GetByKey(key);
            _repository.Delete(entityToRemove);
        }

        public void Update(InType t)
        {
            var entityToUpdate = _mapper.MapBack(t);
            _repository.Update(entityToUpdate);
        }

        public async Task<List<OutType>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public async Task<OutType> GetByKeyAsync(string key)
        {
            return await Task.Run(() => GetByKey(key));
        }

        public async Task<OutType> AddAsync(InType t)
        {
            return await Task.Run(() => Add(t));
        }

        public async Task RemoveAsync(string key)
        {
            await Task.Run(() => Remove(key));
        }

        public async Task UpdateAsync(InType t)
        {
            await Task.Run(() => Update(t));
        }        
    }
}
