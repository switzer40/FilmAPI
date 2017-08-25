using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmPersonService
    {
        System.Collections.Generic.List<FilmPersonViewModel> GetAll();
        Task<List<FilmPersonViewModel>> GetAllAsync();
        FilmPersonViewModel Add(FilmPersonViewModel m);
        Task<FilmPersonViewModel> AddAsync(FilmPersonViewModel m);
        void Delete(FilmPersonViewModel m);
        void Delete(string key);
        Task DeleteAsync(FilmPersonViewModel m);
        Task DeleteAsync(string key);
        void Update(FilmPersonViewModel m);
        Task UpdateAsync(FilmPersonViewModel m);
        FilmPersonViewModel GetBySurrogateKey(string key);
        Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key);
    }
}
