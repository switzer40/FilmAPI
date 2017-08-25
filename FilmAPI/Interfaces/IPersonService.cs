using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IPersonService
    {
        System.Collections.Generic.List<PersonViewModel> GetAll();
        Task<List<PersonViewModel>> GetAllAsync();
        PersonViewModel Add(PersonViewModel m);
        Task<PersonViewModel> AddAsync(PersonViewModel m);
        void Delete(PersonViewModel m);
        void Delete(string key);
        Task DeleteAsync(PersonViewModel m);
        Task DeleteAsync(string key);
        void Update(PersonViewModel m);
        Task UpdateAsync(PersonViewModel m);
        PersonViewModel GetBySurrogateKey(string key);
        Task<PersonViewModel> GetBySurrogateKeyAsync(string key);
    }
}
