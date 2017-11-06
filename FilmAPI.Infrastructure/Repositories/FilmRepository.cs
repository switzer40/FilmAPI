using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public override Film GetByKey(string key)
        {
            IKeyService keyService = new KeyService();
            (string title,
             short year) = keyService.DeconstructFilmKey(key);
            return GetByTitleAndYear(title, year);
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            return List(f => f.Title == title && f.Year == year).SingleOrDefault();
        }

        public override Film GetStoredEntity(Film t)
        {
            return GetByTitleAndYear(t.Title, t.Year);
        }

        public override void Update(Film t)
        {
            var storedFilm = GetByTitleAndYear(t.Title, t.Year);
            storedFilm.Copy(t);
            Save();
        }
    }
}
