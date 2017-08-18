using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Threading.Tasks;
using System.Linq;
using FilmAPI.Core.Specifications;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            var spec = new FilmByTitleAndYear(title, year);
            return List(spec).Single();
        }

        public async Task<Film> GetByTitleAndYearAsync(string title, short year)
        {
            var spec = new FilmByTitleAndYear(title, year);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }
    }
}
