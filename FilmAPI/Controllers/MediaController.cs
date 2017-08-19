using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("api/media")]
    public class MediaController : Controller
    {
        private readonly IMediumService _service;
        public MediaController(IMediumService service)
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
            var model = _service.GetBySurrogateKeyAsync(key);
            return Ok(model);
        }
    }
}
