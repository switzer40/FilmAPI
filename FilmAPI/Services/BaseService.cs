using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;
        protected readonly IKeyService _keyService;
        private bool _isValid;
        public BaseService(IRepository<T> repo,
                           IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }
        public bool IsValid { get => _isValid; set => _isValid = value; }

        public abstract OperationResult<IKeyedDto> Add(IBaseDto b);
        
        public async Task<OperationResult<IKeyedDto>> AddAsync(IBaseDto b)
        {
            return await Task.Run(() => Add(b));
        }

        public async Task<OperationStatus> ClearAllAsync()
        {
            var (status, value) = await _repository.ListAsync();
            if (status != OperationStatus.OK)
            {
                return status;
            }
            foreach (T item in value)
            {
                await DeleteAsync(item);
            }
            return OperationStatus.OK;
        }

        public OperationResult<int> Count()
        {
            var (status, value) = _repository.List();
            if (status != OperationStatus.OK)
            {
                return new OperationResult<int>(status);
            }
            var count = value.Count;
            return new OperationResult<int>(status, count);
        }

        public async Task<OperationResult<int>> CountAsync()
        {
            return await Task.Run(() => Count());
        }

        public OperationResult<IKeyedDto> Delete(string key)
        {
            var s = _repository.Delete(key);
            return new OperationResult<IKeyedDto>(s, null);
        }

        public OperationResult<IKeyedDto> Delete(T t)
        {
            var s = _repository.Delete(t);
            return new OperationResult<IKeyedDto>(s, null);
        }

        public async Task<OperationResult<IKeyedDto>> DeleteAsync(string key)
        {

            var decodedKey = System.Net.WebUtility.UrlDecode(key);
            return await Task.Run(() => Delete(decodedKey));
        }

        public async Task<OperationResult<IKeyedDto>> DeleteAsync(T t)
        {
            return await Task.Run(() => Delete(t));
        }

        public OperationResult<List<IKeyedDto>> GetAbsolutelyAll()
        {
            var (status, value) = _repository.List();
            if (status != OperationStatus.OK)
            {
                return new OperationResult<List<IKeyedDto>>(status);
            }
            var val = new List<IKeyedDto>();
            foreach (T item in value)
            {
                IKeyedDto dto = RecoverKeyedEntity(item);
                val.Add(dto);
            }
            return new OperationResult<List<IKeyedDto>>(status, val);
        }

        protected abstract IKeyedDto RecoverKeyedEntity(T item);
        

        public async Task<OperationResult<List<IKeyedDto>>> GetAbsolutelyAllAsync()
        {
            return await Task.Run(() => GetAbsolutelyAll());
        }

        public OperationResult<List<IKeyedDto>> GetAll(int pageIndex, int pageSize)
        {
            var (status, value) = _repository.List();
            if (status != OperationStatus.OK)
            {
                return new OperationResult<List<IKeyedDto>>(status);
            }
            var entities = value
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
            var val = new List<IKeyedDto>();
            foreach (T item in entities)
            {
                IKeyedDto dto = RecoverKeyedEntity(item);
                val.Add(dto);
            }
            return new OperationResult<List<IKeyedDto>>(status, val);
        }

        public async Task<OperationResult<List<IKeyedDto>>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() => GetAll(pageIndex, pageSize));
        }

        public abstract OperationResult<IKeyedDto> GetByKey(string key);
        

        public async Task<OperationResult<IKeyedDto>> GetByKeyAsync(string key)
        {
            var decodedKey = System.Net.WebUtility.UrlDecode(key);
            return await Task.Run(() => GetByKey(decodedKey));
        }

        public abstract OperationStatus Update(IBaseDto b);
        

        public async Task<OperationStatus> UpdateAsync(IBaseDto b)
        {
            return await Task.Run(() => Update(b));
        }

        public abstract OperationResult<IKeyedDto> GetLastEntry();
        

        public async Task<OperationResult<IKeyedDto>> GetLastEntryAsync()
        {
            return await Task.Run(() => GetLastEntry());
        }
    }
}
