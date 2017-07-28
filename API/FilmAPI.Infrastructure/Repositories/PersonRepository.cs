using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context) : base(context)
        {
        }

        public Person GetByLastNameAndBirthdate(string lastNane, string birthdate)
        {
            return List(p => (p.LastName == lastNane && p.BirthdateString == birthdate)).Single();
        }
    }
}
