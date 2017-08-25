using FilmAPI.Core.Entities;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/films")]
    [ValidateModel]
    public class FilmsController : Controller
    {
        public FilmsController()
        {
        }
        private readonly IFilmService _service;
        public FilmsController(IFilmService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var films = await _service.GetAllAsync();
            return Ok(films);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var person = await _service.GetBySurrogateKeyAsync(key);
            return Ok(person);
        }
    }
}
