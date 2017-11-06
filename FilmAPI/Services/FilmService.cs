using FilmAPI.Common.DTOs.Film;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class FilmService : EntityService<Film, BaseFilmDto, KeyedFilmDto>
    {
        public FilmService(IFilmRepository repo, BaseMapper<Film, BaseFilmDto> mapper) : base(repo, mapper)
        {
        }
        protected override KeyedFilmDto GenerateOutType(Film f)
        {
            var key = _keyService.ConstructFilmKey(f.Title, f.Year);
            return new KeyedFilmDto(f.Title, f.Year, f.Length, key);
        }
    }
}
