using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetByLastNameAndBirthdate(string lastName, string birthdate);
        Person GetByKey(string key);
    }
}
