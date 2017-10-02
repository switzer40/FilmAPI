using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Interfaces.Read
{
    public partial interface IReadFilmPersonRepository
    {
        List<FilmPerson> GetAll();
        List<FilmPerson> GetAll(Expression<Func<FilmPerson, bool>> predicate);
        List<FilmPerson> GetAll(ISpecification<FilmPerson> specification);
        FilmPerson GetById(int id);
        FilmPerson GetByTitleYearLastNameBirthdateAndRole(string title,
                                                          short year,
                                                          string lastName,
                                                          string birthdate,
                                                          string role);
    }
}
