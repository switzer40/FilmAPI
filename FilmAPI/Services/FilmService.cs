using FilmAPI.Common.DTOs.Film;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Film;
using FilmAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class FilmService : EntityService<Film, BaseFilmDto, KeyedFilmDto>, IFilmService
    {
        public FilmService(IFilmRepository repo, IFilmMapper mapper) : base(repo, (BaseMapper<Film, BaseFilmDto>)mapper)
        {
        }
        protected override KeyedFilmDto GenerateOutType(Film f)
        {
            var key = _keyService.ConstructFilmKey(f.Title, f.Year);
            return new KeyedFilmDto(f.Title, f.Year, f.Length, key);
        }
    }
}
