using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IFilmService : IEntityService<Film, FilmViewModel>
    {
    }
}
