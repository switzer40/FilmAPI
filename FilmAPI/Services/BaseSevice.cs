using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Common.Services;

namespace FilmAPI.Services
{
    public abstract class BaseSevice<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;
        protected readonly IKeyService _keyService;
        public BaseSevice(IRepository<T> repo,
                          IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public IKeyedDto<T> Add(IBaseDto<T> b)
        {
            var entityToAdd = _mapper.MapBack(b);
            var savedEntity = _repository.Add(entityToAdd);
            var savedModel = _mapper.Map(savedEntity);
            return ExtractKeyedDto(savedModel);
        }

        protected abstract IKeyedDto<T> ExtractKeyedDto(IBaseDto<T> b);
        

        public async Task<IKeyedDto<T>> AddAsync(IBaseDto<T> b)
        {
            return await Task.Run(() => Add(b));
        }

        public void Delete(string key)
        {
            var entityToDelete = _repository.GetByKey(key);
            _repository.Delete(entityToDelete);
        }

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key));
        }

        public List<IKeyedDto<T>> GetAll()
        {
            var result = new List<IKeyedDto<T>>();
            var entities = _repository.List();
            var models = _mapper.MapList(entities);
            foreach (var m in models)
            {
                result.Add(ExtractKeyedDto(m));
            }
            return result;
        }

        public async Task<List<IKeyedDto<T>>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public IKeyedDto<T> GetByKey(string key)
        {
            var entity = _repository.GetByKey(key);
            var model = _mapper.Map(entity);
            return ExtractKeyedDto(model);
        }

        public async Task<IKeyedDto<T>> GetByKeyAsync(string key)
        {
            return await Task.Run(() => GetByKey(key));
        }

        public void Update(IBaseDto<T> b)
        {
            var entityToUpdate = _mapper.MapBack(b);
            var storedEntity = RetrieveStoredEntity(b);
            storedEntity.Copy(entityToUpdate);
        }

        protected abstract T RetrieveStoredEntity(IBaseDto<T> b);

        public async Task UpdateAsync(IBaseDto<T> b)
        {
            await Task.Run(() => Update(b));
        }
    }
}
