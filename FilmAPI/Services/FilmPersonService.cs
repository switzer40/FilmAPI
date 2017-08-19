using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.ViewModels;
using FilmAPI.Core.Entities;

namespace FilmAPI.Services
{
    public class FilmPersonService : IFilmPersonService
    {
        public FilmPersonViewModel Add(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task<FilmPersonViewModel> AddAsync(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public void Delete(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }


        public List<FilmPersonViewModel> GetAall()
        {
            throw new NotImplementedException();
        }

        public List<FilmPersonViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<FilmPersonViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public FilmPersonViewModel GetBySurrogateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<FilmPersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }

        Task IEntityService<FilmPerson, FilmPersonViewModel>.DeleteAsync(FilmPersonViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
