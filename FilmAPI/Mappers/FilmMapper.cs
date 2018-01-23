using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class FilmMapper : BaseMapper<Film>, IFilmMapper
    {
        public override IBaseDto Map(Film t)
        {
            return new BaseFilmDto(t.Title, t.Year, t.Length);
        }

        public override Film MapBack(IBaseDto dto)
        {
            var b = (BaseFilmDto)dto;
            return new Film(b.Title, b.Year, b.Length);
        }
    }
}