using FilmAPI.Core.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.Common.Services;
using FluentValidation.Results;
using FilmAPI.Common.Utilities;
using System.Linq;
using System;

namespace FilmAPI.Services
{
    public abstract class BaseSevice<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;
        protected readonly IKeyService _keyService;
        protected List<IKeyedDto> result;
        private bool _isValid;
        

        public bool IsValid { get => _isValid; set => _isValid = value; }

        public BaseSevice(IRepository<T> repo,
                          IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
            _isValid = false;
            result = new List<IKeyedDto>();            
        }
        public abstract OperationResult Add(IBaseDto b);


        protected abstract IKeyedDto ExtractKeyedDto(IBaseDto b);


        public async Task<OperationResult> AddAsync(IBaseDto b)
        {
            return await Task.Run(() => Add(b));
        }

        public abstract OperationResult Delete(string key);


        public async Task<OperationResult> DeleteAsync(string key)
        {
            return await Task.Run(() => Delete(key));
        }

        public OperationResult GetAll()
        {            
            var entities = _repository.List();
            var models = _mapper.MapList(entities.ToList());
            foreach (var m in models)
            {
                result.Add(ExtractKeyedDto(m));
            }
            return StandardResult(OperationStatus.OK);
        }

        protected OperationResult StandardResult(OperationStatus s)
        {
            return new OperationResult(s, result);
        }

        public async Task<OperationResult> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }
        
        public abstract OperationResult Update(IBaseDto b);


        protected abstract T RetrieveStoredEntity(IBaseDto b);

        public async Task<OperationResult> UpdateAsync(IBaseDto b)
        {
            return await Task.Run(() => Update(b));
        }

        public OperationResult  Count()
        {
            return GetAll();
        }

        public async Task<OperationResult> CountAsync()
        {
            return await Task.Run(() => Count());
        }
        public abstract OperationResult GetByKey(string key);
        
        public async Task<OperationResult> GetByKeyAsync(string key)
        {
            return await Task.Run(() => GetByKey(key));
        }

        public abstract OperationResult ClearAll();
        public async Task<OperationResult> ClearAllAsync()
        {
            return await Task.Run(() => ClearAll());
        }        
    }
}
