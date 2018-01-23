using FilmAPI.Common.Services;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public Film GetByKey(string key)
        {
            var keyService = new KeyService();
            var data = keyService.DeconstructFilmKey(key);
            return GetByTitleAndYear(data.title, data.year);
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            var spec = new FilmByTitleAndYear(title, year);
            return List(spec).SingleOrDefault();
        }
    }
}
