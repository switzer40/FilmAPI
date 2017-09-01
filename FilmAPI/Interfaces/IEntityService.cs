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
        List<ModelType> GetAll();
        Task<List<ModelType>> GetAllAsync();
        ModelType Add(ModelType m);
        Task<ModelType> AddAsync(ModelType m);
        void Delete(ModelType m);
        void Delete(string key);
        Task DeleteAsync(ModelType m);
        Task DeleteAsync(string key);
        void Update(ModelType m);
        Task UpdateAsync(ModelType m);        
        ModelType GetBySurrogateKey(string key);
        Task<ModelType> GetBySurrogateKeyAsync(string key);
    }
}
