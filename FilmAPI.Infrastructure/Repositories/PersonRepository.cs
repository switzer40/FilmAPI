using FilmAPI.Common.Utilities;
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

        public override OperationStatus Delete(string key)
        {
            var res = GetByKey(key);
            if (res.status != OperationStatus.OK)
            {
                return res.status;
            }
            var personToDelete = res.value;
            return Delete(personToDelete);
        }

        public (OperationStatus status, Person value) GetByKey(string key)
        {
            var data = _keyService.DeconstructPersonKey(key);
            return GetByLastNameAndBirthdate(data.lastName, data.birthdate);
        }

        public (OperationStatus status, Person value) GetByLastNameAndBirthdate(string lastName, string birthdate)
        {
            ISpecification<Person> spec = new PersonByLastNameAndBirthdate(lastName, birthdate);
            var (status, value) = List(spec);
            var p = value.SingleOrDefault();
            return (status, p);
        }

        public override OperationStatus Update(Person t)
        {
            Person storedPerson = default;
            var (status, value) = GetByLastNameAndBirthdate(t.LastName, t.BirthdateString);
            if (status == OperationStatus.OK)
            {
                storedPerson = value;
                storedPerson.Copy(t);
                Save();
            }
            return status;
        }
    }
}
