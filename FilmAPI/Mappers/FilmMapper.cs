using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Mappers
{
    public class FilmMapper : BaseMapper<Film>, IFilmMapper
    {
        public override IBaseDto<Film> Map(Film t)
        {
            var result = new BaseFilmDto(t.Title, t.Year, t.Length);
            return (IBaseDto<Film>)result;
        }

        public override Film MapBack(IBaseDto<Film> dto)
        {
            var b = (BaseFilmDto)dto;
            return new Film(b.Title, b.Year, b.Length);
        }
    }
}
