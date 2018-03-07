using FilmAPI.Filters;
using FilmAPI.Interfaces;
using FilmAPI.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmAPI.Common.Utilities;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;

namespace FilmAPI.Controllers
{
    [Route("/api/Film/")]
    public class FilmController : BaseController<Film>
    {
        private readonly IFilmService _service;
        public FilmController(IFilmService service)
        {
            _service = service;
        }
        [HttpPost("Add")]
        [ValidateFilmNotDuplicate]
        public async Task<OperationResult<IKeyedDto>> PostAsync([FromBody]BaseFilmDto model)
        {
            return await _service.AddAsync(model);            
        }

        [HttpDelete("ClearAll")]
        public async Task<OperationStatus> DeleteAsync()
        {
            return await _service.ClearAllAsync();            
        }
        [HttpDelete("Delete/{key}")]
        [ValidateFilmExists]
        public async Task<OperationStatus> DeleteAsync(string key)
        {
             return await _service.DeleteAsync(key);            

        }
        [HttpGet("GetAbsolutelyAll")]
        public async Task<OperationResult<List<IKeyedDto>>> GetAsync()
        {
            return await _service.GetAbsolutelyAllAsync();
        }
        [HttpGet("GetAll")]
        public async Task<OperationResult<List<IKeyedDto>>> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
             return await _service.GetAllAsync(pageIndex, pageSize);            
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmExists]
        public async Task<OperationResult<IKeyedDto>> GetAsync(string key)
        {
            return await _service.GetByKeyAsync(key);                        
        }

        [HttpGet("Count")]
        public async Task<OperationResult<int>> GetAsync(int dummy)
        {
            return await _service.CountAsync();            
        }
        [HttpPut("Edit")]
        [ValidateFilmToUpdateExists]
        public async Task<OperationStatus> Put([FromBody]BaseFilmDto model)
        {

            return await _service.UpdateAsync(model);
        }

       
    }
}
