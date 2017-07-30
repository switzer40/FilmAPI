using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using FilmAPI.Infrastructure.Specifications;
using System.Threading.Tasks;

namespace FilmAPI.Infrastructure.Repositories
{
    public class MediumRepository : Repository<Medium>, IMediumRepository
    {
        public MediumRepository(FilmContext context, IKeyService keyService) : base(context, keyService)
        {
        }

        public async Task<Medium> GetByFilmIdAndTypeAsync(int filmId, string type)
        {
            return (await ListAsync(new MediumSpecificationByFilmIdAndMediumType(filmId, type))).Single();
        }

        public Medium GetByFilmIdAndType(int filmId, string type)
        {
            return List(new MediumSpecificationByFilmIdAndMediumType(filmId, type)).Single();
        }

        public override Medium GetBySurrogateKey(string key)
        {
            _keyService.DeconstructzMediumSurrogateKey(key);
            int filmId = _keyService.MediumFilmId;
            string mediumType = _keyService.MediumType;
            return GetByFilmIdAndType(filmId, mediumType);
        }

        public override async Task<Medium> GetBySurrogateKeyAsync(string key)
        {
            _keyService.DeconstructzMediumSurrogateKey(key);
            int filmId = _keyService.MediumFilmId;
            string mediumType = _keyService.MediumType;
            return await GetByFilmIdAndTypeAsync(filmId, mediumType);
        }
    }
}
