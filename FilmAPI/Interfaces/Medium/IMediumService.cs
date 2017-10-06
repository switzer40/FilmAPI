using FilmAPI.DTOs.Film;
using FilmAPI.DTOs.Medium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumService
    {
        KeyedMediumDto Add(DTOs.Medium.BaseMediumDto m , bool force = false);
        Task<KeyedMediumDto> AddAsync(DTOs.Medium.BaseMediumDto m, bool force = false);
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
