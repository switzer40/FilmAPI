using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IMediumRepository :IRepository<Medium>
    {
        Medium GetByFilmIdAndType(int filmId, string type);
        Task<Medium> GetByFilmIdAndTypeAsync(int filmId, string type);
    }
}
