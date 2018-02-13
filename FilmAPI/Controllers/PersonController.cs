using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("/api/Person")]
    public class PersonController : BaseController<KeyedPersonDto>
    {
        private readonly IPersonService _service;
        public PersonController(IPersonService service)
        {
            _service = service;
        } 
        [HttpDelete("ClearAll")]
        public async Task<IActionResult> DeleteAsync()
        {
            var res = await _service.CountAsync();
            return Ok(res);
        }
        [HttpDelete("Delete/{key}")]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var res = await _service.DeleteAsync(key);
            return StandardReturn(res);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            var res = await _service.GetAllAsync(pageIndex, pageSize);
                            
            var people = new List<KeyedPersonDto>();
            foreach (var p in res.Value)
            {
                people.Add((KeyedPersonDto)p);
            }
            return Ok(people);
        }
        [HttpGet("GetByKey/{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var res = await _service.GetByKeyAsync(key);
            var val = (KeyedPersonDto)res.Value;            
            return StandardReturn(res.Status, val);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetAsync(int dummy)
        {
            var res = await _service.CountAsync();
            return StandardCountReturn(res.Status, res.Value);
        }
        [HttpPost("Add")]
        [ValidatePersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BasePersonDto model)
        {
            var res = await _service.AddAsync(model);
            var val = (KeyedPersonDto)res.Value;
            return StandardReturn(res.Status, val);
        }
        [HttpPut("Edit")]
        [ValidatePersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BasePersonDto model)
        {
            var s = await _service.UpdateAsync(model);
            return StandardReturn(s);
        }

    }
}
