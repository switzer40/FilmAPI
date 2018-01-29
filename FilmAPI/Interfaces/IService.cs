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
        OperationResult GetAll();
        Task<OperationResult> GetAllAsync();        
        OperationResult Add(IBaseDto b);
        OperationResult Count();
        Task<OperationResult> CountAsync();
        Task<OperationResult> AddAsync(IBaseDto b);
        OperationResult Delete(string key);
        Task<OperationResult> DeleteAsync(string key);
        Task<OperationResult> GetByKeyAsync(string key);        
        OperationResult Update(IBaseDto b);
        Task<OperationResult> UpdateAsync(IBaseDto b);        
        Task<OperationResult> ClearAllAsync();
        bool IsValid { get; set; }        
    }
}
