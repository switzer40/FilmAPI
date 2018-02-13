using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
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
    public class FilmController : BaseController<KeyedFilmDto>
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
            var val = (KeyedFilmDto)res.Value;
            return StandardReturn(res.Status, val);
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
        public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            var res = await _service.GetAllAsync(pageIndex, pageSize);
            var films = new List<KeyedFilmDto>();
            foreach (var f in res.Value)
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
            var val = (KeyedFilmDto)res.Value;
            return StandardReturn(res.Status, val);            
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetAsync(int dummy)
        {
            var res = await _service.CountAsync();
            return StandardCountReturn(res.Status, res.Value); 
        }
        [HttpPut("Edit")]
        [ValidateFilmToUpdateExists]
        public async Task<IActionResult> Put([FromBody]BaseFilmDto model)
        {
            var s = await _service.UpdateAsync(model);
            return StandardReturn(s);
        }
    }
}
