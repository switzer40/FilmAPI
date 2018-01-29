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
    public class FilmPersonController : BaseController, IController
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
            var res = await _service.GetAllAsync();
            return CountReturn(res);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _service.GetAllAsync();
            var filmPeople = new List<KeyedFilmPersonDto>();
            foreach (var fp in res.ResultValue)
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
            return StandardReturn(res);
        }
        [HttpPost("Add")]
        [ValidateFilmPersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseFilmPersonDto model)
        {
            var res = await _service.AddAsync(model);
            return StandardReturn(res);
        }
        [HttpPut("Edit")]
        [ValidateFilmPersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseFilmPersonDto model)
        {
            var res = await _service.UpdateAsync(model);
            return StandardReturn(res);
        }
    }
}
