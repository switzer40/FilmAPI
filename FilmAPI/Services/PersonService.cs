using AutoMapper;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class PersonService : EntityService<Person, PersonViewModel>, IPersonService
    {
        public PersonService(IPersonRepository repo, IMapper mapper, IKeyService keyService) : base(mapper, keyService)
        {
            _repository = repo;
        }

        public override Person CreateEntity(string key)
        {
            var data = GetData(key);            
            return new Person(data.lastName, data.birthdate);
        }

        private (string lastName, string birthdate) GetData(string key)
        {
            return _keyService.DeconstructPesonSurrogateKey(key);
        }

        public override PersonViewModel EntityToModel(Person e)
        {
            string key = _keyService.ConstructPersonSurrogateKey(e.LastName, e.BirthdateString);
            return new PersonViewModel(e);
        }

        public override Person GetEntity(string key)
        {
            var data = GetData(key);            
            return ((IPersonRepository)_repository).GetByLastNameAndBirthdate(data.lastName, data.birthdate);
        }

        public override Person ModelToEntity(PersonViewModel m)
        {
            return _mapper.Map<Person>(m);
        }

        public override PersonViewModel AddForce(PersonViewModel m)
        {
            return Add(m);
        }
    }
}
