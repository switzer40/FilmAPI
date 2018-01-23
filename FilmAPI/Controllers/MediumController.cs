using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
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
    [Route("/api/Medium/")]
    public class MediumController : NewBaseController<Medium>, IMediumController
    {
        private readonly IKeyService _keyService;
        public MediumController(IMediumService service, IErrorService eservice) : base(eservice)
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
        [ValidateMediumExists]
        public async Task<IActionResult> DeleteAsync(string  key)
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
        [ValidateMediumNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BaseMediumDto model)
        {
            var s = await _service.AddAsync(model);
            if (s == OperationStatus.OK)
            {
                var medium = (KeyedMediumDto)((IMediumService)_service).Result();
                return Ok(medium);
            }
            else
            {
                return HandleError(s);
            }
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> PutAsync([FromBody]BaseMediumDto model)
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

        [HttpGet("GetAll")]
        public async Task<List<IKeyedDto>> GetAsync()
        {
            var list = await _service.GetAllAsync();
            var result = new List<IKeyedDto>();
            foreach (var item in list)
            {
                var dto = (KeyedMediumDto)item;
                dto.Key = _keyService.ConstructMediumKey(dto.Title, dto.Year, dto.MediumType);
                result.Add(dto);
            }
            return result;
        }
        [HttpGet("GetByKey/{key}")]
        [ValidateMediumExists]
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
