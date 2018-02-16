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
    [Route("/api/Medium/")]
    public class MediumController : BaseController<KeyedMediumDto>
    {
        private readonly IMediumService _service;
        public MediumController(IMediumService service)
        {
            _service = service;
        }
        [HttpDelete("ClearAll")]
        public async Task<OperationStatus> DeleteAsync()
        {
            return await _service.ClearAllAsync();            
        }
        [HttpDelete("Delete/{key}")]
        [ValidateMediumExists]
        public async Task<OperationStatus> DeleteAsync(string key)
        {
            return await _service.DeleteAsync(key);            
        }
        [HttpGet("Count")]
        public async Task<OperationResult<int>> GetAsync(int dummy)
        {
            return await _service.CountAsync();            
        }
        [HttpGet("GetAll")]
        public async Task<OperationResult<List<IKeyedDto>>> GetAsync(int pageIndex = 0, int pageSize = 4)
        {
            return await _service.GetAllAsync(pageIndex, pageSize);            
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateMediumExists]
        public async Task<OperationResult<IKeyedDto>> GetAsync(string key)
        {
            return await _service.GetByKeyAsync(key);            
        }
        [HttpPost("Add")]
        [ValidateMediumNotDuplicate]
        public async Task<OperationResult<IKeyedDto>> PostAsync([FromBody]BaseMediumDto model)
        {
            return await _service.AddAsync(model);            
        }
        [HttpPut("Edit")]
        [ValidateMediumToUpdateExists]
        public async Task<OperationStatus> PutAsync([FromBody]BaseMediumDto model)
        {
            return await _service.UpdateAsync(model);            
        }
    }
}
