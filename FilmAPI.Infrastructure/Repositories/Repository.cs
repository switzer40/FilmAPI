using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
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
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FilmContext _context;
        private readonly DbSet<T> _set;
        protected IKeyService _keyService;
        public Repository(FilmContext context)
        {
            _context = context;
            _set = _context.Set<T>();
            _keyService = new KeyService();
        }
        public (OperationStatus status, T value) Add(T t)
        {
            _set.Add(t);
            Save();
            return (OperationStatus.OK, t);
        }

        protected void Save()
        {
            _context.SaveChanges();
        }

        public Task<(OperationStatus status, T value)> AddAsync(T t)
        {
            throw new NotImplementedException();
        }

        public void ClearAll()
        {
            var entitiesToClaar = new List<T>();
            foreach (var entity in _set)
            {
                entitiesToClaar.Add(entity);
            }
            _set.RemoveRange(entitiesToClaar);
            Save();
        }

        public OperationStatus Delete(T t)
        {
            _set.Remove(t);
            Save();
            return OperationStatus.OK;
        }
        public abstract OperationStatus Delete(string key);
        

        public async Task<OperationStatus> DeleteAsync(T t)
        {
            return await Task.Run(() => Delete(t));
        }

        public (OperationStatus status, T value) GetById(int id)
        {
            var val = _set.Find(id);
            var status = OperationStatus.OK;
            if (val == null)
            {
                status = OperationStatus.NotFound;
                status.ReasonForFailure = $"An entity with id = {id} is not present";
            }
            return (status, val);
        }

        public Task<(OperationStatus status, T value)> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public (OperationStatus status, List<T> value) List()
        {
            var val = new List<T>();
            foreach (var item in _set)
            {
                val.Add(item);
            }
            return (OperationStatus.OK, val);
        }

        public (OperationStatus status, List<T> value) List(Expression<Func<T, bool>> predicate)
        {
            var val = _set.Where(predicate).ToList();
            return (OperationStatus.OK, val);
        }

        public (OperationStatus status, List<T> value) List(ISpecification<T> specification)
        {
            var val = _set.Where(specification.Predicate).ToList();
            return (OperationStatus.OK, val);
        }

        public async Task<(OperationStatus status, List<T> value)> ListAsync()
        {
            return await Task.Run(() => List());
        }

        public async Task<(OperationStatus status, List<T> value)> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => List(predicate));
        }

        public async Task<(OperationStatus status, List<T> value)> ListAsync(ISpecification<T> specification)
        {
            return await Task.Run(() => List(specification));
        }

        public abstract OperationStatus Update(T t);
        

        public async Task<OperationStatus> UpdateAsync(T t)
        {
            return await Task.Run(() => Update(t));
        }

       
        public async Task<OperationStatus> DeleteAsync(string key)
        {
            return await Task.Run(() => Delete(key));
        }

        public (OperationStatus status, T value) GetLastEntry()
        {
            var val = _set.LastOrDefault();
            return (OperationStatus.OK, val);
        }

        public async Task<(OperationStatus status, T value)> GetLastEntryAsync()
        {
            return await Task.Run(() => GetLastEntry());
        }
    }
}
