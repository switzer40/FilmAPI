using FilmAPI.DTOs;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FilmAPI.DTOs.Film;

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
        [ValidateFilmExists]
        public async Task<IActionResult> Get(string key)
        {
            KeyedFilmDto model = await _service.GetBySurrogateKeyAsync(key);           
            return Ok(model);
        }
        [HttpPost]
        [ValidateFilmNotDuplicate]
        public async Task<IActionResult> Post([FromBody] KeyedFilmDto model)
        {            
            var savedModel = await _service.AddAsync(model);
            return Ok(savedModel);
        }
        [HttpPut]
        [ValidateFilmToUpdateExists]
        public async Task<IActionResult> Put([FromBody] KeyedFilmDto model)
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
