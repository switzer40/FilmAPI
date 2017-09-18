using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IEntityService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel     
    {
        EntityType CreateEntity(string key);
        EntityType ModelToEntity(ModelType m);
        EntityType GetEntity(string key);
        ModelType EntityToModel(EntityType e);
        List<ModelType> GetAll();
        Task<List<ModelType>> GetAllAsync();
        ModelType Add(string key);
        ModelType Add(ModelType m);
        Task<ModelType> AddAsync(string key);
        Task<ModelType> AddAsync(ModelType m);
        // The AddForce... methods are needed by the entity Medium
        ModelType AddForce(string key);
        ModelType AddForce(ModelType m);
        Task<ModelType> AddForceAsync(string key);
        Task<ModelType> AddForceAsync(ModelType m);
        void Delete(string key);        
        Task DeleteAsync(string key);        
        void Update(string key);
        void Update(ModelType m);
        Task UpdateAsync(string key);
        Task UpdateAsync(ModelType m);
        ModelType GetBySurrogateKey(string key);
        Task<ModelType> GetBySurrogateKeyAsync(string key);
    }
}
