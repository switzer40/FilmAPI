using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/films")]
    public class FilmsController : Controller
    {
        private readonly IFilmService _service;
        public FilmsController(IFilmService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await _service.GetAllAsync();
            return Ok(models);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] FilmViewModel model)
        {
            var savedModel = await _service.AddAsync(model);
            return Ok(savedModel);
        }
    }
}
