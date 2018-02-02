using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("/api/Film/")]
    public class FilmController : BaseController, IController
    {
        private readonly IFilmService _service;
        public FilmController(IFilmService service)
        {
            _service = service;
        }
        [HttpPost("Add")]
        [ValidateFilmNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseFilmDto model)
        {
            var res = await _service.AddAsync(model);
            return StandardReturn(res);
        }

        [HttpDelete("ClearAll")]
        public async Task<IActionResult> DeleteAsync()
        {
            var s = await _service.ClearAllAsync();
            return StandardReturn(s);
        }
        [HttpDelete("Delete/{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var s = await _service.DeleteAsync(key);
            return StandardReturn(s);

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _service.GetAllAsync();
            var films = new List<KeyedFilmDto>();
            foreach (var f in res.ResultValue)
            {
                films.Add((KeyedFilmDto)f);
            }
            return Ok(films);
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> GetAsync(string key)
        {
            var res = await _service.GetByKeyAsync(key);
            return StandardReturn(res);            
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetAsync(int dummy)
        {
            var res = await _service.GetAllAsync();
            return CountReturn(res); ;
        }
        [HttpPut("Edit")]
        [ValidateFilmToUpdateExists]
        public async Task<IActionResult> Put([FromBody]BaseFilmDto model)
        {
            var res = await _service.UpdateAsync(model);
            return StandardReturn(res);
        }
    }
}
