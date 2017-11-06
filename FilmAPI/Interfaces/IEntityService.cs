using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IEntityService<EntityType, InType, OutType>
        where EntityType : BaseEntity
        where InType :  IBaseDto
        where OutType : IKeyedDto
    {
        List<OutType> GetAll();
        Task<List<OutType>> GetAllAsync();
        OutType GetByKey(string key);
        Task<OutType> GetByKeyAsync(string key);
        OutType Add(InType t);
        Task<OutType> AddAsync(InType t);
        void Remove(string key);
        Task RemoveAsync(string key);
        void Update(InType t);
        Task UpdateAsync(InType t);
    }
}
