using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        (OperationStatus status, Film value) GetByTitleAndYear(string title, short year);
        (OperationStatus status, Film value) GetByKey(string key);
    }
}
