using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Interfaces.Read
{
    public partial interface IReadPersonRepository
    {
        List<Person> GetAll();
        List<Person> GetAll(Expression<Func<Person, bool>> predicate);
        List<Person> GetAll(ISpecification<Person> specification);
        Person GetById(int id);
        Person GeetByLastNameAndBirthdate(string lastName, string birthdate);
    }
}
