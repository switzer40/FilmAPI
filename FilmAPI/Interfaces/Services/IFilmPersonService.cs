using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Services
{
    public interface IFilmPersonService : IService<FilmPerson>
    {
        object Result();
    }
}
