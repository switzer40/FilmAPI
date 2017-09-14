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
        
        private readonly IFilmService _service;
        private readonly IKeyService _keyService;
        public FilmsController(IFilmService service, IKeyService keyService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _keyService = keyService;
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
            FilmViewModel model = null;
            try
            {
                model = await _service.GetBySurrogateKeyAsync(key);
            }
            catch
            {
                return BadRequest(key);
            }            
            return Ok(model);
        }
        [HttpPost]
        [ValidateFilmNotDuplicate]
        public async Task<IActionResult> Post([FromBody] FilmViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructFilmSurrogateKey(model.Title, model.Year);
            var savedModel = await _service.AddAsync(model);
            return Ok(savedModel);
        }
        [HttpPut("{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> Put(string key, [FromBody] FilmViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructFilmSurrogateKey(model.Title, model.Year);
            await _service.UpdateAsync(model.SurrogateKey);
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
