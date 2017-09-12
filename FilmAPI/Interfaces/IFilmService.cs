using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmService 
    {
       
        FilmViewModel Add(string key);
        FilmViewModel Add(FilmViewModel m);
        Task<FilmViewModel> AddAsync(string key);
        Task<FilmViewModel> AddAsync(FilmViewModel m);
        void Delete(string key);
        Task DeleteAsync(string key);
        List<FilmViewModel> GetAll();
        Task<List<FilmViewModel>> GetAllAsync();
        FilmViewModel GetBySurrogateKey(string key);
        Task<FilmViewModel> GetBySurrogateKeyAsync(string key);
        void Update(string key);
        void Update(FilmViewModel m);
        Task UpdateAsync(string key);
        Task UpdateAsync(FilmViewModel m);
    }
}
