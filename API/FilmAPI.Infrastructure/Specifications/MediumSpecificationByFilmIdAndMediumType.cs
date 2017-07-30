using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FilmAPI.Infrastructure.Specifications
{
    public class MediumSpecificationByFilmIdAndMediumType : ISpecification<Medium>
    {
        public MediumSpecificationByFilmIdAndMediumType(int filmId, string mediumType)
        {
            FilmId = filmId;
            MediumType = mediumType;
        }
        public int FilmId { get; set; }
        public string MediumType { get; set; }
        public Expression<Func<Medium, bool>> Predicate => (m) => ((m.FilmId == FilmId) && (m.MediumType == MediumType));
    }
}
