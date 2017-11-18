using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Common.Constants;
using FilmAPI.Filters;
using FilmAPI.Filters.Person;
using FilmAPI.Interfaces.Services;

namespace FilmAPI.Controllers
{
    [Route(FilmConstants.PersonUri)]
    [ValidateModel]
    public class PersonController : BaseController<Person>, IPersonController
    {
        public PersonController(IPersonService service) : base(service)
        {
        }
        [HttpDelete("{key}")]
        [ValidatePersonExists]
        public override async Task<IActionResult> DeleteAsync(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var people = await _service.GetAllAsync();
            return Ok(people);
        }
        [HttpGet("{key}")]
        [ValidatePersonExists]
        public override async Task<IActionResult> GetByKeyAsync(string key)
        {
            var person = await _service.GetByKeyAsync(key);
            return Ok(person);
        }
        [HttpPost]
        [ValidatePersonNotDuplicate]
        public override async Task<IActionResult> PostAsync([FromBody] IBaseDto<Person> b)
        {
            var savedPerson = await _service.AddAsync(b);
            return Ok(savedPerson);
        }
        [HttpPut]
        [ValidateFilmToUpdateExists]
        public override async Task<IActionResult> PutAsync([FromBody] IBaseDto<Person> b)
        {
            await _service.UpdateAsync(b);
            return Ok();
        }
    }
}
