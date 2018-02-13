using FilmAPI.Common.Utilities;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void ClearAll();
        (OperationStatus status, T value) Add(T t);
        Task<(OperationStatus status, T value)> AddAsync(T t);
        OperationStatus Delete(T t);
        OperationStatus Delete(string key);
        Task<OperationStatus> DeleteAsync(T t);
        Task<OperationStatus> DeleteAsync(string key);
        (OperationStatus status, T value) GetById(int id);
        Task<(OperationStatus status, T value)> GetByIdAsync(int id);
        (OperationStatus status, List<T> value) List();
        (OperationStatus status, List<T> value) List(Expression<Func<T, bool>> predicate);
        (OperationStatus status, List<T> value) List(ISpecification<T> specification);
        Task<(OperationStatus status, List<T> value)> ListAsync();
        Task<(OperationStatus status, List<T> value)> ListAsync(Expression<Func<T, bool>> predicate);
        Task<(OperationStatus status, List<T> value)> ListAsync(ISpecification<T> specification);
        OperationStatus Update(T t);
        Task<OperationStatus> UpdateAsync(T t);        
    }
}
