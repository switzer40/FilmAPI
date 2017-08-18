using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Core.Specifications
{
    public class FilmPersonByFilmIdPersonIdAndRole : ISpecification<FilmPerson>
    {
        private int _filmId;
        private int _personId;
        private string _role;
        public FilmPersonByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            _filmId = filmId;
            _personId = personId;
            _role = role;
        }
        public Expression<Func<FilmPerson, bool>> Predicate => (fp) => (fp.FilmId == _filmId && fp.PersomId == _personId && fp.Role == _role);
    }
}
