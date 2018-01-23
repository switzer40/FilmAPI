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
        List<IKeyedDto> GetAll();
        Task<List<IKeyedDto>> GetAllAsync();
        
        OperationStatus Add(IBaseDto b);
        int Count();
        Task<int> CountAsync();
        Task<OperationStatus> AddAsync(IBaseDto b);
        OperationStatus Delete(string key);
        Task<OperationStatus> DeleteAsync(string key);
        Task<OperationStatus> GetByKeyAsync(string key);
        IKeyedDto GetByKeyResult(string key);
        OperationStatus Update(IBaseDto b);
        Task<OperationStatus> UpdateAsync(IBaseDto b);
        bool IsValid { get; set; }        
    }
}
