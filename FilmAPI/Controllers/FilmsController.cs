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
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmViewModel model)
        {
            var savedModel = await _service.AddAsync(model);
            return Ok(savedModel);
        }
        [HttpPut("{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> Put(string key, [FromBody] FilmViewModel model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
            
    }
}
