using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmAPI.Core.Interfaces
{
    public interface IFilmPersonRepository : IRepository<FilmPerson>
    {
        FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role);
        Task<FilmPerson> GetByFilmIdPersonIdAndRoleAsync(int filmId, int personId, string role);
    }
}
