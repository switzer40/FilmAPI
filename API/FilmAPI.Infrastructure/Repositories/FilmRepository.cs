using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Infrastructure.Specifications;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context, IKeyService keyService) : base(context, keyService)
        {
        }

        public override Film GetBySurrogateKey(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            string title = _keyService.FilmTitle;
            short year = _keyService.FilmYear;
            return GetByTitleAndYear(title, year);
        }

        public override async Task<Film> GetBySurrogateKeyAsync(string key)
        {
            _keyService.DeconstructFilmSurrogateKey(key);
            string title = _keyService.FilmTitle;
            short year = _keyService.FilmYear;
            return await GetByTitleAndYearAsync(title, year);
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            return List(new FilmSüpecificationByTitleAndYear(title, year)).Single();
        }

        public async Task<Film> GetByTitleAndYearAsync(string title, short year)
        {
            return (await ListAsync(new FilmSüpecificationByTitleAndYear(title, year))).Single();
        }
    }
}
