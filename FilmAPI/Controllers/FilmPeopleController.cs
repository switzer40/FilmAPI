using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    [Route("api/filmpeople")]
    [ValidateModel]
    public class FilmPeopleController : Controller
    {
        private readonly IFilmPersonService _service;
        public FilmPeopleController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var filmPeople = await _service.GetAllAsync();
            return Ok(filmPeople);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var filmPerson = await _service.GetBySurrogateKeyAsync(key);
            return Ok(filmPerson);
        }
    }
}
