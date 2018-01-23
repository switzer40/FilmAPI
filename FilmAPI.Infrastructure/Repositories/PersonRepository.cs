using FilmAPI.Common.Services;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmAPI.Infrastructure.Repositories
{
   public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(FilmContext context) : base(context)
        {
        }

        public Person GetByKey(string key)
        {
            var keyService = new KeyService();
            var data = keyService.DeconstructPersonKey(key);
            return GetByLastNameAndBirthdate(data.lastName, data.birthdate);
        }

        public Person GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            var spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            return List(spec).SingleOrDefault();
        }
    }
}
