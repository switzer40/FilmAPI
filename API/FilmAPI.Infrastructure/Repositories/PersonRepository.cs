using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Specifications;

namespace FilmAPI.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context, IKeyService keyService) : base(context, keyService)
        {
        }

        public Person GetByLastNameAndBirthdate(string lastNane, string birthdate)
        {
            return List(new PersonSpecificationByLastNameAndBirthdate(lastNane, birthdate)).Single();
        }

        public async Task<Person> GetByLastNameAndBirthdateAsync(string lastName, string birthdate)
        {
            return (await ListAsync(new PersonSpecificationByLastNameAndBirthdate(lastName, birthdate))).Single();
        }

        public override Person GetBySurrogateKey(string key)
        {
            _keyService.DeconstructPersonSurrogateKey(key);
            string lastName = _keyService.PersonLastName;
            string birthdate = _keyService.PersonBirthdate;
            return GetByLastNameAndBirthdate(lastName, birthdate);
        }

        public override async Task<Person> GetBySurrogateKeyAsync(string key)
        {
            _keyService.DeconstructPersonSurrogateKey(key);
            string lastName = _keyService.PersonLastName;
            string birthdate = _keyService.PersonBirthdate;
            return await GetByLastNameAndBirthdateAsync(lastName, birthdate);
        }
    }
}
