using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Core.Interfaces;
using FilmAPI.Common.Services;

namespace FilmAPI.Services
{
    public abstract class BaseService<T> : IService where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper<T> _mapper;
        protected readonly IKeyService _keyService;
        protected IKeyedDto result;
        private bool _isValid;
        public bool IsValid { get => _isValid; set => _isValid = value; }
        public BaseService(IRepository<T> repo, IMapper<T> mapper)
        {
            _repository = repo;
            _mapper = mapper;
            _keyService = new KeyService();
        }

        public abstract OperationResult<IKeyedDto> Add(IBaseDto dto);
        
        protected abstract IKeyedDto RecoverKeyedEntity(T value);
                        
        public async Task<OperationResult<IKeyedDto>> AddAsync(IBaseDto b)
        {
            return await Task.Run(() => Add(b));
        }

        public Task<OperationStatus> ClearAllAsync()
        {
            throw new NotImplementedException();
        }

        public OperationResult<int> Count()
        {
            var status = GetAbsolutelyAll().Status;
            var list = GetAbsolutelyAll().Value;
            var count = list.Count;
            return new OperationResult<int>(status, count);
        }
        

        public async Task<OperationResult<int>> CountAsync()
        {
            return await Task.Run(() => Count());
        }

        public abstract OperationStatus Delete(string key);
        

        public async Task<OperationStatus> DeleteAsync(string key)
        {
            return await Task.Run(() => Delete(key));
        }

        public async Task<OperationResult<List<IKeyedDto>>> GetAbsolutelyAllAsync()
        {
            return await Task.Run(() => GetAbsolutelyAll());
        }

        public OperationResult<List<IKeyedDto>> GetAll(int pageIndex, int pageSize)
        {
            var status = GetAbsolutelyAll().Status;
            var list = (GetAbsolutelyAll().Value
                .Skip(pageIndex * pageSize)
                .Take(pageSize)).ToList();
            return new OperationResult<List<IKeyedDto>>(status, list);
        }

        public async Task<OperationResult<List<IKeyedDto>>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() => GetAll(pageIndex, pageSize));
        }

        public async Task<OperationResult<IKeyedDto>> GetByKeyAsync(string key)
        {
            return await Task.Run(() => GetByKey(key));
        }
        

        public abstract OperationResult<List<IKeyedDto>> GetAbsolutelyAll();


        public abstract OperationStatus Update(IBaseDto b);
       

        public async Task<OperationStatus> UpdateAsync(IBaseDto b)
        {
            return await Task.Run(() => Update(b));
        }

        public abstract OperationResult<IKeyedDto> GetByKey(string key);       
    }
}
