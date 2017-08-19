using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/filmpeople")]
    public class FilmPersonController : Controller
    {
        private readonly IFilmPersonService _service;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await _service.GetAllAsync();            
            return Ok(models);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var model = await _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
    }
}
