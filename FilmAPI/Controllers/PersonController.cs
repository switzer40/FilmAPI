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
        public async Task<OperationStatus> DeleteAsync()
        {
            return await _service.ClearAllAsync();            
        }
        [HttpDelete("Delete/{key}")]
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
        public async Task<OperationResult<IKeyedDto>> GetAsync(string key)
        {
            return await _service.GetByKeyAsync(key);            
        }
        [HttpGet("Count")]
        public async Task<OperationResult<int>> GetAsync(int dummy)
        {
            return await _service.CountAsync();            
        }
        [HttpPost("Add")]
        [ValidatePersonNotDuplicate]
        public async Task<OperationResult<IKeyedDto>> PostAsync([FromBody]BasePersonDto model)
        {
            return await _service.AddAsync(model);
            
        }
        [HttpPut("Edit")]
        [ValidatePersonToUpdateExists]
        public async Task<OperationStatus> PutAsync([FromBody]BasePersonDto model)
        {
            return await _service.UpdateAsync(model);            
        }

    }
}
