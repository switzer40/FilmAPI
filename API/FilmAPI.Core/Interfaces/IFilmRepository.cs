using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        Film GetByTitleAndYear(string title, short year);
    }
}
