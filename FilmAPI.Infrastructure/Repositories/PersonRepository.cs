using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Core.Interfaces;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context) : base(context)
        {
        }

        public Person GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            return List(p => (p.LastName == lastName && p.BirthdateString == birthdate)).Single();
        }

        public async Task<Person> GetByLastNameAndBirthdateAsync(string lastName, string birthdate)
        {
            var candidates = await ListAsync(p => (p.LastName == lastName && p.BirthdateString == birthdate));
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }
    }
}
