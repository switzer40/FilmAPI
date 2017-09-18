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
        ModelType Add(ModelType m);        
        Task<ModelType> AddAsync(ModelType m);
        // The AddForce... methods are needed by the entity Medium       
        ModelType AddForce(ModelType m);        
        Task<ModelType> AddForceAsync(ModelType m);
        void Delete(string key);        
        Task DeleteAsync(string key);                
        void Update(ModelType m);        
        Task UpdateAsync(ModelType m);
        ModelType GetBySurrogateKey(string key);
        Task<ModelType> GetBySurrogateKeyAsync(string key);
    }
}
