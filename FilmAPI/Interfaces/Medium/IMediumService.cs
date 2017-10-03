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
        KeyedMediumDto Add(BaseMediumDto m);
        Task<KeyedMediumDto> AddAsync(BaseMediumDto m);
        void Delete(string key);
        Task DeleteAsync(string key);
        List<KeyedMediumDto> GetAll();
        Task<List<KeyedMediumDto>> GetAllAsync();
        KeyedMediumDto GetBySurrogateKey(string key);
        Task<KeyedMediumDto> GetBySurrogateKeyAsync(string key);
        void Update(KeyedMediumDto m);
        Task UpdateAsync(KeyedMediumDto m);
    }
}
