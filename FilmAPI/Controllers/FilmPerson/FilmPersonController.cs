using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Filters;
using FilmAPI.Filters.FilmPerson;
using FilmAPI.Interfaces.FilmPerson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FilmAPI.Controllers.FilmPerson
{
    [Route(FilmConstants.FilmPersonUri)]
    [ValidateModel]
    public class FilmPersonController : Controller
    {
        private readonly IFilmPersonService _service;
        private bool _force = false;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
            _force = FilmConstants.Force;
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
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
        [HttpPost]
        [ValidateFilmPersonNotDuplicate]
        public async Task<IActionResult> Post([FromBody] BaseFilmPersonDto model)
        {
            var savedModel = await _service.AddAsync(model, _force);
            return Ok(savedModel);
        }
        [Obsolete("Try Delete(oldFp) followed by Post(newFp)")]
        [HttpPut]
        [ValidateFilmPersonToUpdateExists]
        public IActionResult Put([FromBody] BaseFilmPersonDto model)
        {
            // To be quite honest I cannot imagine what an update on
            // a FilmPererson should do; I recommend a
            // Delete(oldFp) followed by a Post(newFp).
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
    }
}
