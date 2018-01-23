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

namespace FilmAPI.Services
{
    public abstract class BaseSevice<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;
        protected readonly IKeyService _keyService;
        protected IKeyedDto result;
        private bool _isValid;
        protected Dictionary<string, IKeyedDto> _getResults;

        public bool IsValid { get => _isValid; set => _isValid = value; }

        public BaseSevice(IRepository<T> repo,
                          IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
            _isValid = false;
            _getResults = new Dictionary<string, IKeyedDto>();
        }
        public abstract OperationStatus Add(IBaseDto b);


        protected abstract IKeyedDto ExtractKeyedDto(IBaseDto b);


        public async Task<OperationStatus> AddAsync(IBaseDto b)
        {
            return await Task.Run(() => Add(b));
        }

        public abstract OperationStatus Delete(string key);


        public async Task<OperationStatus> DeleteAsync(string key)
        {
            return await Task.Run(() => Delete(key));
        }

        public List<IKeyedDto> GetAll()
        {
            var result = new List<IKeyedDto>();
            var entities = _repository.List();
            var models = _mapper.MapList(entities.ToList());
            foreach (var m in models)
            {
                result.Add(ExtractKeyedDto(m));
            }
            return result;
        }

        public async Task<List<IKeyedDto>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }
        
        public abstract OperationStatus Update(IBaseDto b);


        protected abstract T RetrieveStoredEntity(IBaseDto b);

        public async Task<OperationStatus> UpdateAsync(IBaseDto b)
        {
            return await Task.Run(() => Update(b));
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() => Count());
        }
        public abstract OperationStatus GetByKey(string key);
        
        public async Task<OperationStatus> GetByKeyAsync(string key)
        {
            return await Task.Run(() => GetByKey(key));
        }

        public IKeyedDto GetByKeyResult(string key)
        {
            IKeyedDto result = null;
            if (_getResults.ContainsKey(key))
            {
                result = _getResults[key];
            }
            return result;
        }
    }
}
