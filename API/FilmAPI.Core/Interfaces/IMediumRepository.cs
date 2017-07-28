using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IMediumRepository :IRepository<Medium>
    {
        Medium GetByFilmIdAndType(int filmId, string type);
    }
}
