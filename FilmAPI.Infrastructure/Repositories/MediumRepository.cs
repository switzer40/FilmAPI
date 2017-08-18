using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using FilmAPI.Core.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using FilmAPI.Core.Specifications;

namespace FilmAPI.Infrastructure.Repositories
{
    public class MediumRepository : Repository<Medium>, IMediumRepository
    {
        public MediumRepository(FilmContext context) : base(context)
        {
        }

        public Medium GetByFilmIdAndMediumType(int filmId, string mediumType)
        {
            var spec = new MediumByFilmIdAndMediumType(filmId, mediumType);
            return List(spec).Single();
        }

        public async Task<Medium> GetByFilmIdAndMediumTypeAsync(int filmId, string mediumType)
        {
            var spec = new MediumByFilmIdAndMediumType(filmId, mediumType);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }
    }
}
