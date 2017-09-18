using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IPersonService
    {
                
        PersonViewModel Add(PersonViewModel m);        
        PersonViewModel AddForce(PersonViewModel m);        
        Task<PersonViewModel> AddAsync(PersonViewModel m);        
        Task<PersonViewModel> AddForceAsync(PersonViewModel m);
        List<PersonViewModel> GetAll();
        Task<List<PersonViewModel>> GetAllAsync();               
        void Delete(string key);      
        Task DeleteAsync(string key);               
        PersonViewModel GetBySurrogateKey(string key);
        Task<PersonViewModel> GetBySurrogateKeyAsync(string key);        
        void Update(PersonViewModel m);        
        Task UpdateAsync(PersonViewModel m);
    }
}
