using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        (OperationStatus status, Person value) GetByLastNameAndBirthdate(string lastName, string birthdate);
        (OperationStatus status, Person value) GetByKey(string key);
    }
}
