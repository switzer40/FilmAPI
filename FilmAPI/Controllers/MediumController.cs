using FilmAPI.Common.DTOs;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("/api/Medium/")]
    public class MediumController : BaseController, IController
    {
        private readonly IMediumService _service;
        public MediumController(IMediumService service)
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
        [ValidateMediumExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var res = await _service.DeleteAsync(key);
            return StandardReturn(res);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetAsync(int dummy)
        {
            var res = await _service.CountAsync();
            return StandardReturn(res);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            var res = await _service.GetAllAsync(pageIndex, pageSize);
            var media = new List<KeyedMediumDto>();
            foreach (var m in res.ResultValue)
            {
                media.Add((KeyedMediumDto)m);
            }
            return Ok(media);
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateMediumExists]
        public async Task<IActionResult> GetAsync(string key)
        {
            var res = await _service.GetByKeyAsync(key);
            var mediumToReturn = res.ResultValue.SingleOrDefault();            
            return StandardReturn(res);
        }
        [HttpPost("Add")]
        [ValidateMediumNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseMediumDto model)
        {
            var res = await _service.AddAsync(model);
            return StandardReturn(res);
        }
        [HttpPut("Edit")]
        [ValidateMediumToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseMediumDto model)
        {
            var res = await _service.UpdateAsync(model);
            return StandardReturn(res);
        }
    }
}
