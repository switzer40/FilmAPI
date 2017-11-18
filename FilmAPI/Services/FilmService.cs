using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Mappers;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Services
{
    public class FilmService : BaseSevice<Film>, IFilmService
    {
        public FilmService(IFilmRepository repo,
                           IFilmMapper mapper) : base(repo, mapper)
        {
        }
        protected override IKeyedDto<Film> ExtractKeyedDto(IBaseDto<Film> dto)
        {
            var b = (BaseFilmDto)dto;
            var key = _keyService.ConstructFilmKey(b.Title, b.Year);
            var result = new KeyedFilmDto(b.Title, b.Year, b.Length, key);
            return (IKeyedDto<Film>)result;
        }

        protected override Film RetrieveStoredEntity(IBaseDto<Film> dto)
        {
            var b = (BaseFilmDto)dto;
            return ((IFilmRepository)_repository).GetByTitleAndYear(b.Title, b.Year);
        }
    }
}
