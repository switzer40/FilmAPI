using FilmAPI.Common.DTOs.Film;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces.Film;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/films")]
    public class FilmsController : BaseController<Film, BaseFilmDto, KeyedFilmDto>
    {
        
        public FilmsController(IFilmService service) : base(service)
        {
        }
    }
}
