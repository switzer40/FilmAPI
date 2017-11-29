using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FilmAPI.Specifications
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
        public Expression<Func<FilmPerson, bool>> Predicate => ((fp) => (fp.FilmId == _filmId && fp.PersonId == _personId && fp.Role == _role));
    }
}
