﻿using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using FilmAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var res = GetByKey(key);
            if (res.status != OperationStatus.OK)
            {
                return res.status;
            }
            return Delete(res.value);
        }

        public (OperationStatus status, Film value) GetByKey(string key)
        {
            var (title, year) = _keyService.DeconstructFilmKey(key);
            return GetByTitleAndYear(title, year);            
        }

        public (OperationStatus status, Film value) GetByTitleAndYear(string title, short year)
        {
            ISpecification<Film> spec = new FilmByTitleAndYear(title, year);
            var (status, value) = List(spec);
            var f = value.SingleOrDefault();
            return (status, f);
        }        
    }
}
