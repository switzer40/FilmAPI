using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using FilmAPI.Core.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using FilmAPI.Core.Specifications;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;



namespace FilmAPI.Infrastructure.Repositories
{
    public class MediumRepository : Repository<Medium>, IMediumRepository
    {
        private readonly IFilmRepository _filmRepository;
        public MediumRepository(FilmContext context, IFilmRepository frepo) : base(context)
        {
            _filmRepository = frepo;
        }

        public int CountMediaByFilmId(int id)
        {
            return List(m => m.FilmId == id).Count();
        }

        public Medium GetByFilmIdAndMediumType(int filmId, string mediumType)
        {
            var spec = new MediumByFilmIdAndMediumType(filmId, mediumType);
            return List(spec).SingleOrDefault();
        }

        public async Task<Medium> GetByFilmIdAndMediumTypeAsync(int filmId, string mediumType)
        {
            return await Task.Run(() => GetByFilmIdAndMediumType(filmId, mediumType));
        }

        public Medium GetByKey(string key)
        {
            IKeyService keyService = new KeyService();
            (string title,
             short year,
             string mediumType) = keyService.DeconstructMediumKey(key);
            return GetByTitleYearAndMediumType(title, year, mediumType);
        }

        public Medium GetByTitleYearAndMediumType(string title, short year, string mediumType)
        {
            var f = _filmRepository.GetByTitleAndYear(title, year);
            if (f == null)
            {
                return null;
            }
            return GetByFilmIdAndMediumType(f.Id, mediumType);
        }
    }
}
