using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
   public interface IMediumRepository : IRepository<Medium>
    {
        Medium GetByFilmIdAndMediumType(int filmId, string mediumType);
        Task<Medium> GetByFilmIdAndMediumTypeAsync(int filmId, string mediumType);
    }
}
