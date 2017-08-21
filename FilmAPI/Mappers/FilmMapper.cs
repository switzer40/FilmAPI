using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class FilmMapper : EntityMapper<Film, FilmViewModel>, IFilmMapper
    {
        private readonly IKeyService _keyService;
        public FilmMapper(IKeyService service)
        {
            _keyService = service;
        }
        public override FilmViewModel Map(Film e)
        {
            return new FilmViewModel(e, _keyService.ConstructFilmSurrogateKey(e.Title, e.Year));
        }

        public override Film MapBack(FilmViewModel m)
        {
            return new Film(m.Title, m.Year);
        }
    }
}
