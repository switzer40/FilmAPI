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
        private readonly IKeyService _keyService;
        public FilmPeopleController(IFilmPersonService service, IKeyService keyService)
        {
            _service = service;
            _keyService = keyService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var filmPeople = await _service.GetAllAsync();
            return Ok(filmPeople);
        }
        [HttpGet("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> Get(string key)
        {
            FilmPersonViewModel model = null;
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
        public async Task<IActionResult> Post([FromBody]FilmPersonViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructFilmPersonSurrogateKey(model.FilmTitle, 
                                                                             model.FilmYear,
                                                                             model.PersonLastName,
                                                                             model.PersonBirthdate,
                                                                             model.Role);
            var savedModel = await _service.AddForceAsync(model.SurrogateKey);
            return Ok(savedModel);
        }
        [HttpPut("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> Put(string key, [FromBody] FilmPersonViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructFilmPersonSurrogateKey(model.FilmTitle,
                                                                             model.FilmYear,
                                                                             model.PersonLastName,
                                                                             model.PersonBirthdate,
                                                                             model.Role);
            await _service.UpdateAsync(model.SurrogateKey);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                await _service.DeleteAsync(key);
            }
            catch (Exception e)
            {
                return BadRequest(key);
            }
            return Ok();
        }
    }
}
