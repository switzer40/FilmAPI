using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmPersonRepository : Repository<FilmPerson>, IFilmPersonRepository
    {
        public FilmPersonRepository(FilmContext context, IKeyService keyService) : base(context, keyService)
        {
        }

        public FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            return List(fp => (fp.FilmId == filmId && fp.PersonId == personId && fp.Role == role)).Single();
        }

        public async Task<FilmPerson> GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role)
        {
            return (await ListAsync(fp => (fp.FilmId == filmId && fp.PersonId == personId && fp.Role == role))).Single();
        }

        public override FilmPerson GetBySurrogateKey(string key)
        {
            string[] parts = DeconstructKey(key);
            int filmId = int.Parse(parts[0]);
            int personId = int.Parse(parts[1]);
            string role = parts[2];
            return GetByFilmIdPersonIdAndRole(filmId, personId, role);

        }
        public override async Task<FilmPerson> GetBySurrogateKeyAsync(string key)
        {
            string[] parts = DeconstructKey(key);
            int filmId = int.Parse(parts[0]);
            int personId = int.Parse(parts[1]);
            string role = parts[2];
            return await GetByFilmIdPersonIdAndRoleAsync(filmId, personId, role);
        }

        private string[] DeconstructKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
