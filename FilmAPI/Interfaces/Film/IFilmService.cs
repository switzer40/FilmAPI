using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmService 
    {
               
        KeyedFilmDto Add(DTOs.Film.BaseFilmDto m);                      
        Task<KeyedFilmDto> AddAsync(BaseFilmDto m);                
        void Delete(string key);
        Task DeleteAsync(string key);
        List<KeyedFilmDto> GetAll();
        Task<List<KeyedFilmDto>> GetAllAsync();
        KeyedFilmDto GetBySurrogateKey(string key);
        Task<KeyedFilmDto> GetBySurrogateKeyAsync(string key);        
        void Update(KeyedFilmDto m);        
        Task UpdateAsync(KeyedFilmDto m);
    }
}
