using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Interfaces.Services;
using FilmAPI.Filters.FilmPerson;

namespace FilmAPI.Controllers
{
    public class FilmPersonController : BaseController<FilmPerson>, IFilmPersonController
    {
        public FilmPersonController(IFilmPersonService service) :base(service)
        {
        }
        [HttpDelete("{key}")]
        public override async Task<IActionResult> DeleteAsync(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var filmPeople = await _service.GetAllAsync();
            return Ok(filmPeople);
        }
        [HttpGet("{key}")]
        [ValidateFilmPersonExists]
        public override async Task<IActionResult> GetByKeyAsync(string key)
        {
            var filmPerson = await _service.GetByKeyAsync(key);
            return Ok(filmPerson);
        }
        [HttpPost]
        [ValidateFilmPersonNotDuplicate]
        public override async Task<IActionResult> PostAsync([FromBody] IBaseDto<FilmPerson> b)
        {
            var savedFilmPerson = await _service.AddAsync(b);
            return Ok(savedFilmPerson);
        }
        [HttpPut]        
        public override async Task<IActionResult> PutAsync([FromBody] IBaseDto<FilmPerson> b)
        {
            // You are advised not to use this action
            //Instead try Delete(oldfp) followed by Add(newfp).
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
