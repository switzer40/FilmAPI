using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetByLastNameAndBirthdate(string lastNane, string birthdate);
        Task<Person> GetByLastNameAndBirthdateAsync(string lastName, string birthdate);
    }
}
