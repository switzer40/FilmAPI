using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Core.Specifications
{
    public class FilmById : ISpecification<Film>
    {
        private int _id;
        public FilmById(int id)
        {
            _id = id;
        }
        public Expression<Func<Film, bool>> Predicate => (f) => f.Id == _id;
    }
}
