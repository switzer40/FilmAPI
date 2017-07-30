using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FilmContext _context;
        private readonly DbSet<T> _set;
        protected readonly IKeyService _keyService;
        public Repository(FilmContext context, IKeyService keyService)
        {
            _context = context;
            _set = _context.Set<T>();
            _keyService = keyService;
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
           await _set.AddAsync(t);
           await SaveAsync();
           return t;
        }

        private async 
        Task
SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(T t)
        {
            _set.Remove(t);
            Save();
        }

        public async Task DeleteAsync(T t)
        {
            _set.Remove(t);
            await SaveAsync();
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }
        public abstract T GetBySurrogateKey(string key);
        public abstract Task<T> GetBySurrogateKeyAsync(string key);
        
        public IEnumerable<T> List()
        {
            return _set;
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate).AsEnumerable();
        }

        public IEnumerable<T> List(ISpecification<T> specification)
        {
            return _set.Where(specification.Predicate);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification)
        {
            return (await ListAsync(specification.Predicate)).AsEnumerable();
        }

        public void Update(T t)
        {
            Save();
        }

        public async Task UpdateAsync(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            await SaveAsync();
        }      
    }
}
