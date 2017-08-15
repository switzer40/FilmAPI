using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Threading.Tasks;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            return List(f => (f.Title == title && f.Year == year)).Single();
        }

        public async Task<Film> GetByTitleAndYearAsync(string title, short year)
        {
            var candidates = await ListAsync(f => (f.Title == title && f.Year == year));
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }
    }
}
