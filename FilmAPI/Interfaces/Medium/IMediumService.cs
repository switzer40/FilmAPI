using FilmAPI.Common.DTOs.Medium;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumService
    {
        KeyedMediumDto Add(BaseMediumDto m , bool force = false);
        Task<KeyedMediumDto> AddAsync(BaseMediumDto m, bool force = false);
        void Delete(string key);
        Task DeleteAsync(string key);
        List<KeyedMediumDto> GetAll();
        Task<List<KeyedMediumDto>> GetAllAsync();
        KeyedMediumDto GetBySurrogateKey(string key);
        Task<KeyedMediumDto> GetBySurrogateKeyAsync(string key);
        void Update(BaseMediumDto m);
        Task UpdateAsync(BaseMediumDto m);
    }
}
