using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.SharedKernel;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        OperationResult<List<IKeyedDto>> GetAbsolutelyAll();
        Task<OperationResult<List<IKeyedDto>>> GetAbsolutelyAllAsync();
        OperationResult<List<IKeyedDto>> GetAll(int pageIndex, int pageSize);
        Task<OperationResult<List<IKeyedDto>>> GetAllAsync(int pageIndex, int pageSize);        
        OperationResult<IKeyedDto> Add(IBaseDto b);
        OperationResult<int> Count();
        Task<OperationResult<int>> CountAsync();
        Task<OperationResult<IKeyedDto>> AddAsync(IBaseDto b);
        OperationStatus Delete(string key);
        OperationStatus Delete(T t);
        Task<OperationStatus> DeleteAsync(string key);
        Task<OperationStatus> DeleteAsync(T t);
        OperationResult<IKeyedDto> GetByKey(string key);
        Task<OperationResult<IKeyedDto>> GetByKeyAsync(string key);        
        OperationStatus Update(IBaseDto b);
        Task<OperationStatus> UpdateAsync(IBaseDto b);        
        Task<OperationStatus> ClearAllAsync();
        bool IsValid { get; set; }
    }
}
