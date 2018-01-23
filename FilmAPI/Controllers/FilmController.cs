using FilmAPI.Common.Utilities;
using FilmAPI.Common.DTOs;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Filters;

namespace FilmAPI.Controllers
{
    [Route("/api/Film")]
    public class FilmController : NewBaseController<Film>, INewFilmController
    {
        private readonly IKeyService _keyService;
        public FilmController(IFilmService service, IErrorService eservice) :base(eservice)
        {
            _service = service;
            _keyService = new KeyService();
        }
        [HttpGet("Count")]
        public async Task<int> GetAsync(int dummy)
        {
            return await _service.CountAsync();
        }
        [HttpDelete("Delete/{key}")]
        [ValidateFilmExists]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var s = await _service.DeleteAsync(key);
            if (s == OperationStatus.OK)
            {
                return Ok();
            }
            else
            {
                return HandleError(s);
            }
        }
                
        [HttpPost("Add")]
        [ValidateFilmNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseFilmDto model)
        {
            var s = await _service.AddAsync(model);
            if (s == OperationStatus.OK)
            {
                var film = ((IFilmService)_service).Result();
                return Ok(film);
            }
            else
            {
                return HandleError(s);
            }
        }
        [HttpPut("Edit")]
        [ValidateFilmToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BaseFilmDto model)
        {
            var s = await _service.UpdateAsync(model);
            if (s== OperationStatus.OK)
            {
                return Ok();
            }
            else
            {
                return HandleError(s);
            }
        }
        [HttpGet("GetAll")]
        public async Task<List<IKeyedDto>> GetAsync()
        {
            var list = await _service.GetAllAsync();
            var result = new List<IKeyedDto>();
            foreach (var item in list)
            {
                var dto = (KeyedFilmDto)item;
                dto.Key = _keyService.ConstructFilmKey(dto.Title,
                                                       dto.Year);
                result.Add(dto);
            }
            return result;
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateFilmExists]
        public async Task<OperationStatus> GetAsync(string key)
        {
            var s = await _service.GetByKeyAsync(key);
            if (s == OperationStatus.OK)
            {
                _getResults[key] = _service.GetByKeyResult(key);
            }
            return s;
        }
    }
}
