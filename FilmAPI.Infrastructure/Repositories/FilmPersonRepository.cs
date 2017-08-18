using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using FilmAPI.Core.Specifications;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmPersonRepository : Repository<FilmPerson>, IFilmPersonRepository
    {
        public FilmPersonRepository(FilmContext context) : base(context)
        {
        }

        public FilmPerson GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            return List(spec).Single();
        }

    
        public async Task<FilmPerson> GetNyFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }
    }
}
