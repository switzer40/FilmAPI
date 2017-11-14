using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Core.Interfaces;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using FilmAPI.Core.Specifications;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;

namespace FilmAPI.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context) : base(context)
        {
        }

        public override Person GetByKey(string key)
        {
            IKeyService keyService = new KeyService();
            (string lastName,
             string birthdate) = keyService.DeconstructPersonKey(key);
            return GetByLastNameAndBirthdate(lastName, birthdate);
        }

        public Person GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            if (lastName == FilmConstants.BADKEY)
            {
                return new Person(lastName, birthdate);
            }
            var spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            return List(spec).SingleOrDefault();
        }

        public async Task<Person> GetByLastNameAndBirthdateAsync(string lastName, string birthdate)
        {
            var spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }

        public override Person GetStoredEntity(Person t)
        {
            return GetByLastNameAndBirthdate(t.LastName, t.BirthdateString);
        }
    }
}
