using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmPersonService
    {        
        List<FilmPersonViewModel> GetAll();
        Task<List<FilmPersonViewModel>> GetAllAsync();
        FilmPersonViewModel Add(string key);
        FilmPersonViewModel Add(FilmPersonViewModel m);
        FilmPersonViewModel AddForce(string key);
        FilmPersonViewModel AddForce(FilmPersonViewModel m);
        Task<FilmPersonViewModel> AddAsync(string key);
        Task<FilmPersonViewModel> AddAsync(FilmPersonViewModel m);
        Task<FilmPersonViewModel> AddForceAsync(string key);
        Task<FilmPersonViewModel> AddForceAsync(FilmPersonViewModel m);
        void Delete(string key);        
        Task DeleteAsync(string key);        
        void Update(string key);
        void Update(FilmPersonViewModel m);
        Task UpdateAsync(string key);
        Task UpdateAsync(FilmPersonViewModel m);
        FilmPersonViewModel GetBySurrogateKey(string key);
        Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key);
    }
}
