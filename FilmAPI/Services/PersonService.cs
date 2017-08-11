using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.ViewModels;

namespace FilmAPI.Services
{
    public class PersonService : IPersonService
    {
        public PersonViewModel Add(PersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task<PersonViewModel> AddAsync(PersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public void Delete(PersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public List<PersonViewModel> GetAall()
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public PersonViewModel GetBySurrogateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<PersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(PersonViewModel m)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PersonViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
