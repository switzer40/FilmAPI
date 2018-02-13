using FilmAPI.Common.DTOs;
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
    [Route("/api/FilmPerson/")]
    public class FilmPersonController : BaseController<KeyedFilmPersonDto>
    {
        private readonly IFilmPersonService _service;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpDelete("ClearAll")]
        public async Task<IActionResult> DeleteAsync()
        {
            var res = await _service.ClearAllAsync();
            return StandardReturn(res);
        }
        [HttpDelete("Delete/{key}")]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var res = await _service.DeleteAsync(key);
            return StandardReturn(res);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetAsync(int dummy)
        {
            var res = await _service.CountAsync();
            return StandardCountReturn(res.Status, res.Value);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            var res = await _service.GetAllAsync(pageIndex, pageSize);
            var filmPeople = new List<KeyedFilmPersonDto>();
            foreach (var fp in res.Value)
            {
                filmPeople.Add((KeyedFilmPersonDto)fp);
            }
            return Ok(filmPeople);
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmPersonExists]
        public async Task<IActionResult> GetAsync(string key)
        {
            var res = await _service.GetByKeyAsync(key);
            var val= (KeyedFilmPersonDto)res.Value;            
            return StandardReturn(res.Status, val);
        }
        [HttpPost("Add")]
        [ValidateFilmPersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseFilmPersonDto model)
        {
            var res = await _service.AddAsync(model);
            var val = (KeyedFilmPersonDto)res.Value;
            return StandardReturn(res.Status, val);
        }
        [HttpPut("Edit")]
        [ValidateFilmPersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseFilmPersonDto model)
        {
            var s = await _service.UpdateAsync(model);            
            return StandardReturn(s);
        }
    }
}
