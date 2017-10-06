using FilmAPI.DTOs.FilmPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonService
    {
        KeyedFilmPersonDto Add(BaseFilmPersonDto b);
        Task<KeyedFilmPersonDto> AddAsync(BaseFilmPersonDto b);
        void Delete(string key);
        Task DeleteAsync(string key);
        List<KeyedFilmPersonDto> GetAll();
        Task<List<KeyedFilmPersonDto>> GetAllAsync();
        KeyedFilmPersonDto GetBySurrogateKey(string key);
        Task<KeyedFilmPersonDto> GetBySurrogateKeyAsync(string key);
        void Update(BaseFilmPersonDto m);
        Task UpdateAsync(BaseFilmPersonDto m);
    }
}
