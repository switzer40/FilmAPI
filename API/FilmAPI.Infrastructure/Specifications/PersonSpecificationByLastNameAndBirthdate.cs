using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Infrastructure.Specifications
{
   
    public class PersonSpecificationByLastNameAndBirthdate : ISpecification<Person>
    {
        public PersonSpecificationByLastNameAndBirthdate(string lastName, string boirthdate)
        {
            LastName = lastName;
            Birthdate = Birthdate;
        }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public Expression<Func<Person, bool>> Predicate => (p) => (p.LastName == LastName && p.BirthdateString == Birthdate);
    }
}
