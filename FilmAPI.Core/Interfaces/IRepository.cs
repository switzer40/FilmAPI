using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FilmAPI.Core.Entities;

namespace FilmAPI.Core.Interfaces
{
     public interface IRepository<T> where T : BaseEntity
    {
        T Add(T t);
        Task<T> AddAsync(T t);
        void Delete(T t);
        Task DeleteAsync(T t);
        void Delete(int id);
        Task DeleteAsync(int id);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetByKey(string key);
        Task<T> GetByKeyAsync(string key);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> predicate);
        List<T> List(ISpecification<T> specification);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> ListAsync(ISpecification<T> specification);
        void Update(T t);
        Task UpdateAsync(T t);

        
    }
}
