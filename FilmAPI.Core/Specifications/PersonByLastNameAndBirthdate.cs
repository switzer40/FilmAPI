using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Specifications
{
    public class PersonByLastNameAndBirthdate : ISpecification<Person>
    {
        private string _lastName;
        private string _birthdate;
        public PersonByLastNameAndBirthdate(string lastName, string birthdate)
        {
            _lastName = lastName;
            _birthdate = birthdate;
        }
        public Expression<Func<Person, bool>> Predicate => (p) => (p.LastName == _lastName && p.BirthdateString == _birthdate);
    }
}
