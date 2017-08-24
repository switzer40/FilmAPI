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
    [Route("api/media")]
    [ValidateModel]
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
            var media = await _service.GetAllAsync();
            return Ok(media);
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var medium = await _service.GetBySurrogateKeyAsync(key);
            return Ok(medium);
        }
    }
}
