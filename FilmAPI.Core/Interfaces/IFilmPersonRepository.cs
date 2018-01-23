using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmPersonRepository : IRepository<FilmPerson>
    {
        FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role);
        FilmPerson GetByKey(string key);
        FilmPerson GetByTitleYearLastNameBirthdateAndRole(string title,
                                                          short year,
                                                          string lastName,
                                                          string birthdate,
                                                          string role);
    }
}
