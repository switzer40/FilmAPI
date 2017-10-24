using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces.Create;
using FilmAPI.Core.Interfaces.Delete;
using FilmAPI.Core.Interfaces.Read;
using FilmAPI.Core.Interfaces.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
   public interface IFilmRepository : IRepository<Film>
    {
        Film GetByTitleAndYear(string title, short year);
    }
}
