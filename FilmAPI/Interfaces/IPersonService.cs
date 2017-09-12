using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IPersonService
    {
        
        PersonViewModel Add(string key);
        PersonViewModel Add(PersonViewModel m);
        Task<PersonViewModel> AddAsync(string key);
        Task<PersonViewModel> AddAsync(PersonViewModel m);
        List<PersonViewModel> GetAll();
        Task<List<PersonViewModel>> GetAllAsync();               
        void Delete(string key);      
        Task DeleteAsync(string key);               
        PersonViewModel GetBySurrogateKey(string key);
        Task<PersonViewModel> GetBySurrogateKeyAsync(string key);
        void Update(string key);
        void Update(PersonViewModel m);
        Task UpdateAsync(string key);
        Task UpdateAsync(PersonViewModel m);
    }
}
