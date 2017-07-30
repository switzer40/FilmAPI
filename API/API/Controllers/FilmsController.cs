using API.ViewModels;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class FilmsController : BaseController<Film, FilmViewModel>
    {
        public FilmsController(IRepository<Film> repository, IMapper mapper) : base(repository, mapper)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rawFilms = await _repository.ListAsync();
            return Ok(_mapper.Map<FilmViewModel>(rawFilms));
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            
            var rawFilm = await _repository.GetBySurrogateKeyAsync(key);
            return Ok(_mapper.Map<FilmViewModel>(rawFilm));
        }
    }
}
