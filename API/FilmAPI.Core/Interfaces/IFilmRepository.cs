using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        Film GetByTitleAndYear(string title, short year);
        Task<Film> GetByTitleAndYearAsync(string title, short year);
    }
}
