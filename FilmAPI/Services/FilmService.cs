using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Services
{
    public class FilmService : EntityService<Film, FilmViewModel>, IFilmService
    {
        public FilmService(IFilmRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
