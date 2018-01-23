using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FilmContext _context;
        private readonly DbSet<T> _set;
        public Repository(FilmContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public T Add(T t)
        {
            _set.Add(t);
            Save();
            return t;
        }

        private void Save()
        {
            _context.SaveChanges();
        }

        public async Task<T> AddAsync(T t)
        {
            return await Task.Run(() => Add(t));
        }

        public void Delete(T t)
        {
            _set.Remove(t);
            Save();
        }

        public async Task DeleteAsync(T t)
        {
            await Task.Run(() => Delete(t));
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Task.Run(() => GetById(id));
        }

        public IEnumerable<T> List()
        {
            return _set;
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return List().Where(predicate.Compile());
        }

        public IEnumerable<T> List(ISpecification<T> specification)
        {
            return List(specification.Predicate);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await Task.Run(() => List());
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => List(predicate));
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification)
        {
            return await Task.Run(() => List(specification));
        }

        public void Update(T t)
        {
            var storedEntity = GetById(t.Id);
            storedEntity.Copy(t);
            Save();
        }

        public async Task UpdateAsync(T t)
        {
            await Task.Run(() => Update(t));
        }
    }
}
