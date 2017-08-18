using FilmAPI.Core.SharedKernel;
using FilmAPI.VviewModls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IEntityService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        List<ModelType> GetAall();
        Task<List<ModelType>> GetAllAsync();
        ModelType Add(ModelType m);
        Task<ModelType> AddAsync(ModelType m);
        void Delete(ModelType m);
        Task DeleteAsync(ModelType m);
        void Update(ModelType m);
        Task UpdateAsync(ModelType m);
        
       ModelType GetBySurrogateKey(string key);
        Task<ModelType> GetBySurrogateKeyAsync(string key);
    }
}
