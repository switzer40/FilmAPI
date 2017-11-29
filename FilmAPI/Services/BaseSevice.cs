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
using FluentValidation.Results;

namespace FilmAPI.Services
{
    public abstract class BaseSevice<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;        
        protected readonly IKeyService _keyService;
        protected IKeyedDto<T> result;
        private bool _isValid;
        private List<ValidationFailure> _failures;

        public bool IsValid { get => _isValid; set => _isValid = value; }
        public List<ValidationFailure> Failures { get => _failures; }

        public BaseSevice(IRepository<T> repo,
                          IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;            
            _keyService = new KeyService();
            _isValid = false;
            _failures = new List<ValidationFailure>();
        }
        public abstract OperationStatus Add(IBaseDto<T> b);
        

        protected abstract IKeyedDto<T> ExtractKeyedDto(IBaseDto<T> b);
        

        public async Task<OperationStatus> AddAsync(IBaseDto<T> b)
        {
            return await Task.Run(() => Add(b));
        }

        public abstract OperationStatus Delete(string key);
        

        public async Task<OperationStatus> DeleteAsync(string key)
        {
            return await Task.Run(() => Delete(key));
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

        public abstract OperationStatus Update(IBaseDto<T> b);
        

        protected abstract T RetrieveStoredEntity(IBaseDto<T> b);

        public async Task<OperationStatus> UpdateAsync(IBaseDto<T> b)
        {
            return await Task.Run(() => Update(b));
        }        
    }
}
