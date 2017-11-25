using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Services
{
    public enum OperationStatus
    {
        OK = 0,
        BadRequest = 1,
        NotFound =2
    }
    public interface IService<T> where T :BaseEntity
    {
        List<IKeyedDto<T>> GetAll();
        Task<List<IKeyedDto<T>>> GetAllAsync();
        IKeyedDto<T> GetByKey(string key);
        Task<IKeyedDto<T>> GetByKeyAsync(string key);
        IKeyedDto<T> Add(IBaseDto<T> b);
        Task<IKeyedDto<T>> AddAsync(IBaseDto<T> b);
        OperationStatus Delete(string key);
        Task<OperationStatus> DeleteAsync(string key);
        OperationStatus Update(IBaseDto<T> b);
        Task<OperationStatus> UpdateAsync(IBaseDto<T> b);
    }
}
