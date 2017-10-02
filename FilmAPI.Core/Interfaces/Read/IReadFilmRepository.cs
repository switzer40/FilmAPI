using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Interfaces.Read
{
    public partial interface IReadFilmRepository
    {
        List<Film> GetAll();
        List<Film> GetAll(Expression<Func<Film, bool>> predicate);
        List<Film> GetAll(ISpecification<Film> specification);
        Film GeById(int id);
        Film GetByTitleAndYear(string title, short year);
    }
}
