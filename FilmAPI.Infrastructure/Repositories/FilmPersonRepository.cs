using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Data;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmPersonRepository : Repository<FilmPerson>, IFilmPersonRepository
    {
        public FilmPersonRepository(FilmContext context) : base(context)
        {
        }

        public FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            return List(spec).SingleOrDefault();
        }

        public async Task<FilmPerson> GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            var spec = new FilmPersonByFilmIdPersonIdAndRole(filmId, personId, role);
            var candidates = await ListAsync(spec);
            var uniqueCandidate = candidates.Single();
            return uniqueCandidate;
        }

        public override FilmPerson GetStoredEntity(FilmPerson t)
        {
            return GetByFilmIdPersonIdAndRole(t.FilmId, t.PersonId, t.Role);
        }

        public override void Update(FilmPerson t)
        {
            var storedFilmPerson = GetByFilmIdPersonIdAndRole(t.FilmId, t.PersonId, t.Role);
            storedFilmPerson.Copy(t);
            Save();
        }
    }
}
