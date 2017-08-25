using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
   public interface IMediumService
    {
        System.Collections.Generic.List<MediumViewModel> GetAll();
        Task<List<MediumViewModel>> GetAllAsync();
        MediumViewModel Add(MediumViewModel m);
        Task<MediumViewModel> AddAsync(MediumViewModel m);
        void Delete(MediumViewModel m);
        void Delete(string key);
        Task DeleteAsync(MediumViewModel m);
        Task DeleteAsync(string key);
        void Update(MediumViewModel m);
        Task UpdateAsync(MediumViewModel m);
        MediumViewModel GetBySurrogateKey(string key);
        Task<MediumViewModel> GetBySurrogateKeyAsync(string key);
    }
}
