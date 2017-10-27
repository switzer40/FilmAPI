using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Interfaces.Read
{
    public partial interface IReadMediumRepository
    {
        List<Medium> GertAll();
        List<Medium> GertAll(Expression<Func<Medium, bool>> predicate);
        List<Medium> GertAll(ISpecification<Medium> specification);
        Medium GetById(int id);
        Medium GetByTitleYearAndMediumType(string title, short year, string mediumType);
    }
}
