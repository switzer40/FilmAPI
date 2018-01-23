using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Specifications
{
    public class FilmPersonByFilmIdPersonIdAndRole : ISpecification<FilmPerson>
    {
        public FilmPersonByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            _filmId = filmId;
            _personId = personId;
            _role = role;
        }
        private int _filmId;
        private int _personId;
        private string _role;
        public System.Linq.Expressions.Expression<Func<FilmPerson, bool>> Predicate => (fp) => fp.FilmId == _filmId &&
                                                                                               fp.PersonId == _personId &&
                                                                                               fp.Role == _role;
    }
}
