using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Threading.Tasks;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public Film GetByTitleAndYear(string title, short year)
        {
            throw new NotImplementedException();
        }

        public Task<Film> GetByTitleAndYearAsync(string title, short year)
        {
            throw new NotImplementedException();
        }
    }
}
