using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Core.Specifications
{
    public class MediumByFilmIdAndMediumType : ISpecification<Medium>
    {
        private int _filmId;
        private string _mediumType;
        public MediumByFilmIdAndMediumType(int filmId, string mediumType)
        {
            _filmId = filmId;
            _mediumType = mediumType;
        }
        public Expression<Func<Medium, bool>> Predicate => (m)=> (m.FilmId == _filmId && m.MediumType == _mediumType);
    }
}
