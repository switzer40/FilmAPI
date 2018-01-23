using FilmAPI.Common.DTOs;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IPersonService : IService<Person>
    {
        object Result();
        KeyedPersonDto GetByLastNameAndBirthdate(string lastName, string birthdate);
    }
}