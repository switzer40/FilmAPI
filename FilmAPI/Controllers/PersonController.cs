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
    [Route("/api/Person/")]
    public class PersonController : NewBaseController<Person>, INewPersonController
    {
        private readonly IKeyService _keyService;
        public PersonController(IPersonService service, IErrorService eservice) : base(eservice)
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
        [ValidatePersonExists]
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
        [ValidatePersonNotDuplicate]
        public async Task<IActionResult> PostAsync([FromBody]BasePersonDto model)
        {
            var s = await _service.AddAsync(model);
            if (s == OperationStatus.OK)
            {
                var person = ((IPersonService)_service).GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                return Ok(person);
            }
            else
            {
                return HandleError(s);
            }
        }
        [HttpPut("Edit")]
        [ValidatePersonToUpdateExists]
        public async Task<IActionResult> PutAsync([FromBody]BasePersonDto model)
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
        async Task<List<IKeyedDto>> INewController.GetAsync()
        {
            var list = await _service.GetAllAsync();
            var result = new List<IKeyedDto>();
            foreach (var item in list)
            {
                var dto = (KeyedPersonDto)item;
                dto.Key = _keyService.ConstructPersonKey(dto.LastName,
                                                         dto.Birthdate);
                result.Add(dto);
            }
            return result;
        }

       [HttpGet("GetByKey/{key}")]
       [ValidatePersonExists]
        public async Task<OperationStatus> GetAsync(string key)
        {
            var s = await _service.GetByKeyAsync(key);
            if (s == OperationStatus.OK)
            {
                _getResults[key] = _service.GetByKeyResult(key);
            }
            return s;
        }
        [HttpGet("GetAll")]
        public async Task<List<IKeyedDto>> GetAsync()
        {
            var list = await _service.GetAllAsync();
            var result = new List<IKeyedDto>();
            foreach (var item in list)
            {
                var dto = (KeyedPersonDto)item;
                dto.Key = _keyService.ConstructPersonKey(dto.LastName, dto.Birthdate);
                result.Add(dto);
            }
            return result;
        }
    }
}
