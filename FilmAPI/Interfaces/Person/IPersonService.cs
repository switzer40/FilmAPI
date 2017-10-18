using FilmAPI.Common.DTOs.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Person
{
    public interface IPersonService
    {
        KeyedPersonDto Add(BasePersonDto m);
        Task<KeyedPersonDto> AddAsync(BasePersonDto m);
        void Delete(string key);
        Task DeeteAsync(string key);
        List<KeyedPersonDto> GetAll();
        Task<List<KeyedPersonDto>> GetAllAsync();
        KeyedPersonDto GetBySurrogateKey(string key);
        Task<KeyedPersonDto> GetBySurrogateKeyAsync(string key);
        void Update(BasePersonDto m);
        Task UpdateAsync(BasePersonDto m);
    }
}
