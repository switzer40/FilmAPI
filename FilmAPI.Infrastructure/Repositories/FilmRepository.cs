using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Infrastructure.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(FilmContext context) : base(context)
        {
        }

        public override OperationStatus Delete(string key)
        {
            throw new NotImplementedException();
        }

        public (OperationStatus status, Film value) GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public (OperationStatus status, Film value) GetByTitleAndYear(string title, short year)
        {
            throw new NotImplementedException();
        }        
    }
}
