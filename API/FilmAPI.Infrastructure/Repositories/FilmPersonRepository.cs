using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmPersonRepository : Repository<FilmPerson>, IFilmPersonRepository
    {
        public FilmPersonRepository(FilmContext context) : base(context)
        {
        }

        public FilmPerson GetByFilmIdPersonIdAndRole(int filmId, int personId, string role)
        {
            return List(fp => (fp.FilmId == filmId && fp.PersonId == personId && fp.Role == role)).Single();
        }
    }
}
