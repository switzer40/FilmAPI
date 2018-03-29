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
    [Route("/api/FilmPerson/")]
    public class FilmPersonController : BaseController<FilmPerson>
    {
        private readonly IFilmPersonService _service;
        public FilmPersonController(IFilmPersonService service)
        {
            _service = service;
        }
        [HttpDelete("ClearAll")]
        public async Task<OperationStatus> DeleteAsync()
        {
            return await _service.ClearAllAsync();            
        }
        [HttpDelete("Delete/{key}")]
        public async Task<OperationResult<IKeyedDto>> DeleteAsync(string key)
        {
            return await _service.DeleteAsync(key);            
        }
        [HttpGet("Count")]
        public async Task<OperationResult<int>> GetAsync(int dummy)
        {
            return await _service.CountAsync();            
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
        [ValidateFilmPersonExists]
        public async Task<OperationResult<IKeyedDto>> GetAsync(string key)
        {
            return await _service.GetByKeyAsync(key);           
        }
        [HttpGet("GetLastEntry")]
        public async Task<OperationResult<IKeyedDto>> GetAsync(short dummy)
        {
            return await _service.GetLastEntryAsync();
        }
        [HttpPost("Add")]
        [ValidateFilmPersonNotDuplicate]
        public async Task<OperationResult<IKeyedDto>> PostAsync([FromBody]BaseFilmPersonDto model)
        {
            return await _service.AddAsync(model);            
        }
        [HttpPut("Edit")]
        [ValidateFilmPersonToUpdateExists]
        public async Task<OperationStatus> PutAsync([FromBody]BaseFilmPersonDto model)
        {
            return await _service.UpdateAsync(model);                        
        }
    }
}
