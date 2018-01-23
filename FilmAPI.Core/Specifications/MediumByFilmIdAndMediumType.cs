using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Specifications
{
    public class MediumByFilmIdAndMediumType : ISpecification<Medium>
    {
        public MediumByFilmIdAndMediumType(int filmId, string mediumType)
        {
            _filmId = filmId;
            _mediumType = mediumType;
        }
        private int _filmId;
        private string _mediumType;
        public Expression<Func<Medium, bool>> Predicate => (m) => m.FilmId == _filmId && m.MediumType == _mediumType;
    }
}
