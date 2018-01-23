using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Filters;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    [Route("/api/[controller]/")]
    public class FilmPersonController : NewBaseController<FilmPerson>, IFilmPersonController
    {
        private readonly IKeyService _keyService;
        public FilmPersonController(IFilmPersonService service, IErrorService eservice) : base(eservice)
        {
            _service = service;
            _keyService = new KeyService();
        }
        [HttpGet("Count")]
        public async Task<int> GetAsync(int dummy)
        {
            return await _service.CountAsync();
        }
        [HttpGet("GetAll")]        
        public async Task<List<IKeyedDto>> GetAsync()
        {
            var list = await _service.GetAllAsync();
            var result = new List<IKeyedDto>();
            foreach (var item in list)
            {
                var dto = (KeyedFilmPersonDto)item;
                dto.Key = _keyService.ConstructFilmPersonKey(dto.Title,
                                                             dto.Year,
                                                             dto.LastName,
                                                             dto.Birthdate,
                                                             dto.Role);
                result.Add(dto);
            }
            return result;
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmPersonExists]
        public async Task<OperationStatus> GetAsync(string key)
        {
            var s = await _service.GetByKeyAsync(key);
            if (s == OperationStatus.OK)
            {
                var fp = _service.GetByKeyResult(key);
                _getResults[key] = fp;                
            }
            return s;
        }

        
        
        [HttpPost("Add")]
        [ValidateFilmPersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody] BaseFilmPersonDto b)
        {
            var s = await _service.AddAsync(b);
            if (s == OperationStatus.OK)
            {
                return Ok(((IFilmPersonService)_service).Result());
            }
            else
            {
                return HandleError(s);
            }
        }
        [HttpPut("Edit")]
        [ValidateFilmPersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody] IBaseDto model)
        {
            var s = await _service.UpdateAsync(model);
            if (s == OperationStatus.OK)
            {
                return Ok();
            }
            else
            {
                return HandleError(s);
            }
        }
    }
}
