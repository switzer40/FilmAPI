using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmService : IService<Film>
    {
        IKeyedDto Result();
        OperationStatus GetByKey(string key);
    }
}