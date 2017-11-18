using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Services
{
    public interface IService<T> where T :BaseEntity
    {
        List<IKeyedDto<T>> GetAll();
        Task<List<IKeyedDto<T>>> GetAllAsync();
        IKeyedDto<T> GetByKey(string key);
        Task<IKeyedDto<T>> GetByKeyAsync(string key);
        IKeyedDto<T> Add(IBaseDto<T> b);
        Task<IKeyedDto<T>> AddAsync(IBaseDto<T> b);
        void Delete(string key);
        Task DeleteAsync(string key);
        void Update(IBaseDto<T> b);
        Task UpdateAsync(IBaseDto<T> b);
    }
}
