using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IMediumRepository : IRepository<Medium>
    {
        (OperationStatus status, Medium value) GetByKey(string key);
        (OperationStatus status, Medium value) GetByFilmIdAndMediumType(int filmId, string mediumType);
        Task<(OperationStatus status, Medium value)> GetByFilmIdAndMediumTypeAsync(int filmId, string mediumType);
        (OperationStatus status, Medium value) GetByTitleYearAndMediumType(string title, short year, string mediumType);        
    }
}
