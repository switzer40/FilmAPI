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
        T Add(T t);
        Task<T> AddAsync(T t);
        void Delete(T t);
        Task DeleteAsync(T t);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        IEnumerable<T> List(ISpecification<T> specification);
        Task<IEnumerable<T>> ListAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification);
        void Update(T t);
        Task UpdateAsync(T t);        
    }
}
