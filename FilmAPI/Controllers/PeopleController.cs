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
    [Route("api/people")]
    [ValidateModel]
    public class PeopleController : Controller
    {
        
        private readonly IPersonService _service;
        private readonly IKeyService _keyService;
        public PeopleController(IPersonService service,IKeyService keyService)
        {
            _service = service;
            _keyService = keyService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _service.GetAllAsync();
            return Ok(people);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructPersonSurrogateKey(model.LastName, model.BirthdateString);
            var savedPerson = await _service.AddAsync(model.SurrogateKey);
            return Ok(savedPerson);
        }
        [HttpPut("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> Put(string key, [FromBody] PersonViewModel model)
        {
            model.SurrogateKey = _keyService.ConstructPersonSurrogateKey(model.LastName, model.BirthdateString);
            await _service.UpdateAsync(model.SurrogateKey);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.DeleteAsync(key);
            return Ok();
        }
    }
}
