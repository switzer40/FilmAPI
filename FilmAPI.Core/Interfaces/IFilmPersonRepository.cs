using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmPersonRepository : IRepository<FilmPerson>
    {
        (OperationStatus status, FilmPerson value) GetByFilmIdPersonIdAndRole(int filmId, int personId, string role);
        (OperationStatus status, FilmPerson value) GetByKey(string key);
        (OperationStatus status, FilmPerson value) GetByTitleYearLastNameBirthdateAndRole(string title,
                                                                                          short year,
                                                                                          string lastName,
                                                                                          string birthdate,
                                                                                          string role);
    }
}
