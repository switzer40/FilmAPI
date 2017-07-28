using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FilmAPI.Infrastructure.Data;
using System.Linq;

namespace FilmAPI.Infrastructure.Repositories
{
    public class MediumRepository : Repository<Medium>, IMediumRepository
    {
        public MediumRepository(FilmContext context) : base(context)
        {
        }

        public Medium GetByFilmIdAndType(int filmId, string type)
        {
            return List(m => (m.FilmId == filmId && m.MediumType == type)).Single();
        }
    }
}
