using FilmAPI.Common.DTOs.FilmPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.FilmPerson
{
    public interface IFilmPersonService : IEntityService<Core.Entities.FilmPerson, BaseFilmPersonDto, KeyedFilmPersonDto>
    {
    }
}
