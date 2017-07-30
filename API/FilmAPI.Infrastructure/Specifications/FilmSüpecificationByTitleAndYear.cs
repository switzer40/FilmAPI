using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Infrastructure.Specifications
{
    public class FilmSüpecificationByTitleAndYear : ISpecification<Film>
    {
        public FilmSüpecificationByTitleAndYear(string title, short year)
        {
            Title = title;
            Year = year;
        }
        public string Title { get; set; }
        public short Year { get; set; }
        public Expression<Func<Film, bool>> Predicate => (f) => (f.Title == Title && f.Year == Year);
    }
}
