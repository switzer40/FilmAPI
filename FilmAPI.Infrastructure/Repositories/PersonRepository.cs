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

namespace FilmAPI.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context) : base(context)
        {
        }

        public Person GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            if (lastName == FilmConstants.BADKEY)
            {
                return new Person(lastName, birthdate);
            }
            var spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            return List(spec).Single();
        }

        public async Task<Person> GetByLastNameAndBirthdateAsync(string lastName, string birthdate)
        {
            var spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }

        public override void Update(Person t)
        {
            var storedPerson = GetByLastNameAndBirthdate(t.LastName, t.BirthdateString);
            storedPerson.Copy(t);
            Save();
        }
    }
}
