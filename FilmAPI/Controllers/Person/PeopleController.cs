using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Filters;
using FilmAPI.Filters.Person;
using FilmAPI.Interfaces.Person;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmAPI.Controllers.Person
{
    [Route(FilmConstants.PersonUri)]
    [ValidateModel]
    public class PeopleController : Controller
    {
        private readonly IPersonService _service;
        public PeopleController(IPersonService service) => _service = service;
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _service.GetAllAsync();
            return Ok(people);
        }
        [HttpGet("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> Get(string key)
        {
            var result = await _service.GetBySurrogateKeyAsync(key);
            return Ok(result);
        }
        [HttpPost]
        [ValidatePersonNotDuplicate]
        public async Task<IActionResult> Post([FromBody] KeyedPersonDto model)
        {
            var result = await _service.AddAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [ValidatePersonToUpdateExists]
        public async Task<IActionResult> Put([FromBody] KeyedPersonDto model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
        [HttpDelete("{key}")]
        [ValidatePersonExists]
        public async Task<IActionResult> Delete(string key)
        {
            await _service.DeeteAsync(key);
            return Ok();
        }
    }
}
