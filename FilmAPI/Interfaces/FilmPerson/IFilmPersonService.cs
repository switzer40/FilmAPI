using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.Common.DTOs.FilmPerson;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonService
    {
        KeyedFilmPersonDto Add(BaseFilmPersonDto b, bool force = false);
        Task<KeyedFilmPersonDto> AddAsync(BaseFilmPersonDto b, bool force = false);
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
