using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.Common.DTOs.Film;

namespace FilmAPI.Interfaces
{
    public interface IFilmService 
    {
               
        KeyedFilmDto Add(BaseFilmDto m);                      
        Task<KeyedFilmDto> AddAsync(BaseFilmDto m);                
        void Delete(string key);
        Task DeleteAsync(string key);
        List<KeyedFilmDto> GetAll();
        Task<List<KeyedFilmDto>> GetAllAsync();
        KeyedFilmDto GetBySurrogateKey(string key);
        Task<KeyedFilmDto> GetBySurrogateKeyAsync(string key);        
        void Update(BaseFilmDto m);        
        Task UpdateAsync(BaseFilmDto m);
    }
}
