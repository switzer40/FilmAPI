using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Interfaces.Services;
using FilmAPI.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.FilmUri)]
    [ValidateModel]
    public class FilmController : BaseController<Film>, IFilmController
    {
        public FilmController(IFilmService service) : base(service)
        {
        }
        [HttpDelete("{key}")]
        [ValidateFilmExists]
        public override async Task<IActionResult> DeleteAsync(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var films = await _service.GetAllAsync();
            return Ok(films);
        }
        [HttpGet("{key}")]
        [ValidateFilmExists]
        public override async Task<IActionResult> GetByKeyAsync(string key)
        {
            var film = await _service.GetByKeyAsync(key);
            return Ok(film);
        }
        [HttpPost]
        [ValidateFilmNotDuplicate]
        public override async Task<IActionResult> PostAsync([FromBody] IBaseDto<Film> b)
        {
            var savedFilm = await _service.AddAsync(b);
            return Ok(savedFilm);
        }
        [HttpPut]
        [ValidateFilmToUpdateExists]
        public override async Task<IActionResult> PutAsync([FromBody] IBaseDto<Film> b)
        {
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
