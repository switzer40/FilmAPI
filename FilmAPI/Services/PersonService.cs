using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.ViewModels;
using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Entities;

namespace FilmAPI.Services
{
    public class PersonService : EntityService<Person, PersonViewModel>, IPersonService
    {
        public PersonService(IPersonRepository repository, IMapper mapper, IKeyService keyService) : base(repository, mapper, keyService)
        {
        }

        public override PersonViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructPesonSurrogateKey(key);
            PersonViewModel model = new PersonViewModel(new Person(_keyService.PersonLastName, _keyService.PersonBirthdate));
            return model;
        }

        public async override Task<PersonViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }
    }
}
